using Posts.Entities;
using Posts.Models;

namespace Posts.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<int> CreatePost(string title, string content, string authorId);
        Task<PaginatedList<Post>> GetUserPosts(string username, int limit, int offset);
        Task<List<Post>> GetAllPosts();
    }
}
