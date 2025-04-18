using Dapper;
using Posts.Data;
using Posts.Entities;
using Posts.Models;
using Posts.Repositories.Interfaces;
using System.Collections.Generic;

namespace Posts.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IDapperDbContext dapperDbContext;

        public PostRepository(IDapperDbContext dapperDbContext)
        {
            this.dapperDbContext = dapperDbContext;
        }

        public async Task<int> CreatePost(string title, string content, string authorId)
        {
            using var connection = dapperDbContext.CreateConnection();

            string str = """
                insert into "Posts"("Title", "Content", "PublishedAt", "AuthorId")
                values(@title, @content, now(), @authorId);
            """;

            int res = await connection.ExecuteAsync(str, new
            {
                title,
                content,
                authorId
            });

            return res;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            using var connection = dapperDbContext.CreateConnection();

            string str = """
                select * from "Posts";
            """;

            var posts = await connection.QueryAsync<Post>(str);

            return posts.ToList();
        }

        public async Task<PaginatedList<Post>> GetUserPosts(string username, int limit, int offset)
        {
            using var connection = dapperDbContext.CreateConnection();

            string str = """
                select * from "Posts"
                where "AuthorId" = @authorId
                limit @limit offset @offset;
            """;

            var posts = await connection.QueryAsync<Post>(str, new
            {
                authorId = username,
                limit,
                offset
            });

            return new PaginatedList<Post>(posts.ToList(), limit, offset, 0);
        }
    }
}
