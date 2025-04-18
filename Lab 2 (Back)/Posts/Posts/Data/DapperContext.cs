using System.Data;
using Npgsql;

namespace Posts.Data
{
    public class DapperContext : IDapperDbContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            string connectionString = "Host=localhost;Port=5432;Database=postDB;Username=postgres;Password=sdfyrf123";

            return new NpgsqlConnection(connectionString);
        }
    }
}
