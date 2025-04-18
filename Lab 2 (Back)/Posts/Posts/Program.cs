using FluentMigrator.Runner;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Posts.Data;
using Posts.Migrations;
using Posts.Repositories;
using Posts.Repositories.Interfaces;
using Posts.Services;
using Posts.Services.Interfaces;
using System.Text;
using static Posts.Migrations.VersionTable;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Database>();
builder.Services.AddSingleton<IDapperDbContext, DapperContext>();

builder.Services.AddTransient<IJwtService, JwtService>();

builder.Services.AddLogging(c => c.AddFluentMigratorConsole())
    .AddFluentMigratorCore()
    .ConfigureRunner(c => c.AddPostgres()
                .WithGlobalConnectionString("Host=localhost;Port=5432;Database=postDB;Username=postgres;Password=sdfyrf123")
                .WithRunnerConventions(new MigrationRunnerConventions())
                .ScanIn(typeof(InitialMigration_202504140001).Assembly).For.Migrations())
    .AddScoped<IVersionTableMetaData, CustomVersionTableMetaData>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        RequireAudience = true,
        ValidIssuer = "post.com",
        ValidAudience = "https://localhost:44380",
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("79a8d031-3f97-4ac3-bc40-fa06b4d6c819"))
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.UseCors("AllowAngularClient");

var db = app.Services.GetRequiredService<Database>();
db.CreateDatabaseIfNotExists();

app.MigrateDb();

app.Run();
