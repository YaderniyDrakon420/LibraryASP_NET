using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Books.Domain.Entities;

namespace Books.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(UserEntity user);
    }

    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly int _expiryMinutes;

        public JwtService(string secret, int expiryMinutes)
        {
            _secret = secret;
            _expiryMinutes = expiryMinutes;
        }

        public string GenerateAccessToken(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_expiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
