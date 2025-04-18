using FluentMigrator.Runner;

namespace Posts.Data
{
    public static class DbManager
    {
        public static IHost MigrateDb(this IHost host)
        {
            using(var scope = host.Services.CreateScope())
            {
                var ms = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                try
                {
                    ms.MigrateUp();
                }
                catch (Exception _) { }
            }

            return host;    
        }
    }
}
