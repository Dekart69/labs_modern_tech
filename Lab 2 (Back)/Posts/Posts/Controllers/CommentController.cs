using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Posts.Repositories.Interfaces;

namespace Posts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public CommentController(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        [HttpPost("[action]")]
        public async Task<int> CreateComment(int postId, string authorId, string content)
        {
            var user = await _userRepository.GetUser(authorId);
            if (user == null)
            {
                return 0;
            }

            var res = await _commentRepository.CreateComment(postId, authorId, content);
        
            return res;
        }
    }
}
