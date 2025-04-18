using Microsoft.IdentityModel.Tokens;
using Posts.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Posts.Services
{
    public class JwtService : IJwtService
    {
        public Task<string> GenerateAsync(string username, string userId, CancellationToken cancellationToken)
        {
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.UserData, username)
            };

            var key = Encoding.UTF8.GetBytes("79a8d031-3f97-4ac3-bc40-fa06b4d6c819");
            var secret = new SymmetricSecurityKey(key);
            var sc = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            var to = new JwtSecurityToken(
                issuer: "post.com",
                audience: "https://localhost:44380",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: sc
                );

            var token = new JwtSecurityTokenHandler().WriteToken(to);

            return Task.FromResult(token);
        }
    }
}
