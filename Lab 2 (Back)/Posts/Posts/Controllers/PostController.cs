using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Posts.Entities;
using Posts.Models;
using Posts.Repositories.Interfaces;
using System.Security.Claims;

namespace Posts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostController(IUserRepository userRepository, IPostRepository postRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<int> Create([FromBody] CreatePostRequest request, CancellationToken cancellationToken)
        {
           string userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var user = await _userRepository.GetUserById(userId);
            if(user == null) 
            {
                return 0;
            }

            int res = await _postRepository.CreatePost(request.Title, request.Content, user.Id);

            return res;
        }

        [HttpGet("[action]")]
        public async Task<PaginatedList<Post>> UserPosts(string username, int limit, int offset, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(username);
            if (user == null)
            {
                return new PaginatedList<Post>(new List<Post>(), 0, 0, 0);
            }

            var posts = await _postRepository.GetUserPosts(user.Id, limit, offset);

            return posts;
        }

        [HttpGet("[action]")]
        public async Task<List<Post>> All(CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetAllPosts();

            return posts;
        }
    }

    public class CreatePostRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
