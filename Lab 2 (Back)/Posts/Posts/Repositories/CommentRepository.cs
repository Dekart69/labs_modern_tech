using Dapper;
using Posts.Data;
using Posts.Repositories.Interfaces;

namespace Posts.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IDapperDbContext dapperDbContext;

        public CommentRepository(IDapperDbContext dapperDbContext)
        {
            this.dapperDbContext = dapperDbContext;
        }

        public async Task<int> CreateComment(int postId, string authorId, string content)
        {
            using var connection = dapperDbContext.CreateConnection();

            string str = """
                insert into "Comments" ("Content", "PublishedAt", "PostId", "AuthorId")
                values(@content, now(), @postId, @authorId);
            """;

            int res = await connection.ExecuteAsync(str, new
            {
                content,
                postId,
                authorId
            });
    
            return res;
        }
    }
}
