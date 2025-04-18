using Dapper;
using Posts.Data;
using Posts.Entities;
using Posts.Repositories.Interfaces;

namespace Posts.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDapperDbContext dapperDbContext;

        public UserRepository(IDapperDbContext dapperDbContext)
        {
            this.dapperDbContext = dapperDbContext;
        }

        public async Task<int> CreateUser(string id, string username, string password)
        {
            using var connection = dapperDbContext.CreateConnection();

            string str = """
                insert into "Users" ("Id", "Created", "Name", "Password")
                values(@id, now(), @name, @password);
            """;
        
            int res = await connection.ExecuteAsync(str,  new
            {
                id,
                name = username,
                password
            });

            return res; 
        }

        public async Task<User?> GetUser(string username)
        {
            using var connection = dapperDbContext.CreateConnection();

            string str = """
                select * from "Users"
                where "Name" = @username;
                """;

            var result = await connection.QueryAsync<User>(str, new {username});

            return result.FirstOrDefault();
        }

        public async Task<int> DeleteUser(string username, string password)
        {
            using var connection = dapperDbContext.CreateConnection();

            string str = """
                delete from "Users" 
                where 
                "Name" = @username
                and "Password" = @password;
            """;

            int res = await connection.ExecuteAsync(str, new
            {
                name = username,
                password
            });

            return res;
        }

        public async Task<User?> GetUserById(string id)
        {
            using var connection = dapperDbContext.CreateConnection();

            string str = """
                select * from "Users"
                where "Id" = @id;
                """;

            var result = await connection.QueryAsync<User>(str, new { id });

            return result.FirstOrDefault();
        }
    }
}
