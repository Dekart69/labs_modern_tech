using Npgsql;

namespace Posts.Data
{
    public class Database
    {
        private readonly IDapperDbContext _context;

        public Database(IDapperDbContext context)
        {
            _context = context;
        }

        public void CreateDatabase() 
        {
            using(var con = _context.CreateConnection())
            {
                var sql = $"CREATE DATABASE postDB WITH OWNER = postgres ENCODING = 'UTF8';";
                using (var cmd = new NpgsqlCommand(sql, (NpgsqlConnection?)con))
                {
                    try
                    {
                        con.Open();
                        var c = cmd.ExecuteScalar();
                        con.Close();
                    }
                    catch (Exception _) { }
                }
            }
        }

        public void CreateDatabaseIfNotExists()
        {
            // Підключення до бази postgres
            var masterConnectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=sdfyrf123";

            using (var con = new NpgsqlConnection(masterConnectionString))
            {
                var sql = "SELECT 1 FROM pg_database WHERE datname = 'postDB';";
                var createSql = "CREATE DATABASE \"postDB\" WITH OWNER = postgres ENCODING = 'UTF8';";

                using (var checkCmd = new NpgsqlCommand(sql, con))
                {
                    con.Open();
                    var exists = checkCmd.ExecuteScalar();
                    if (exists == null)
                    {
                        using (var createCmd = new NpgsqlCommand(createSql, con))
                        {
                            createCmd.ExecuteNonQuery();
                        }
                    }
                    con.Close();
                }
            }
        }
    }
}
