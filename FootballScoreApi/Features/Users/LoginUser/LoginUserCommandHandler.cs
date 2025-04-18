using FootballScoreApp.DbConnection;
using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Services.IServices;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace FootballScoreApp.Features.Users.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponseDto?>
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(
            AppDbContext context,
            IConfiguration configuration,
            ITokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<TokenResponseDto?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.username);

            if (user is null)
            {
                return null;
            }

            if (user.Username != request.username)
            {
                return null;
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var response = new TokenResponseDto
            {
                AccessToken = _tokenService.CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };

            return response;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new Byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _context.SaveChangesAsync();
            return refreshToken;
        }
    }
}
