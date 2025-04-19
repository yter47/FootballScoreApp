using FootballScoreApp.Entities;
using FootballScoreApp.Services.IServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FootballScoreApp.Services
{
    public sealed class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, user.Username),
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            claims.AddRange(user.UserRoles.Select(ur => new Claim(ClaimTypes.Role, ur.Role.Name)));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSettings:Token")!)
            );

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("JwtSettings:Issuer"),
                audience: _configuration.GetValue<string>("JwtSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public RefreshToken GenerateRefreshToken(User user)
        {
            var randomNumber = new Byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var base64Token = Convert.ToBase64String(randomNumber);

            return new RefreshToken
            {
                UserId = user.Id,
                Token = base64Token,
                RefreshTokenExpiryTimeUtc = DateTime.UtcNow.AddDays(7)
            };
        }
    }
}
