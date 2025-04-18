using System.Runtime.CompilerServices;

namespace Posts.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        public Task<int> CreateComment(int postId, string authorId, string content);
    }
}
