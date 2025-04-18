using Posts.Entities;

namespace Posts.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUser(string username);
        Task<User?> GetUserById(string id);
        Task<int> CreateUser(string id, string username, string password);
        Task<int> DeleteUser(string username, string password);
    }
}
