using Microsoft.AspNetCore.Mvc;
using Posts.Repositories.Interfaces;
using Posts.Services.Interfaces;

namespace Posts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthorizeController(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        /// <summary>
        /// генерація токену
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<string?> GetToken(string username, string password, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(username);

            if(user == null || user.Password != password)
            {
                return null;
            }

            return await _jwtService.GenerateAsync(username, user.Id, cancellationToken);
        }
        
        /// <summary>
        /// реєстрація нового користувача
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<string?> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var idGuid= Guid.NewGuid();
        
            int c = await _userRepository.CreateUser(idGuid.ToString(), request.Username, request.Password);

            if(c == 0)
            {
                return null;
            }

            return await _jwtService.GenerateAsync(request.Username, idGuid.ToString(), cancellationToken);
        }
        
        /// <summary>
        /// видалення користувача
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public async Task<bool> Delete(string username, string password, CancellationToken cancellationToken)
        {
            int res = await _userRepository.DeleteUser(username, password);
        
            if(res == 0)
            {
                return false;
            }

            return true;
        }
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
