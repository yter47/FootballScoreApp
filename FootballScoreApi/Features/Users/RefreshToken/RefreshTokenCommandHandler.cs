using FootballScoreApp.DbConnection;
using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Services.IServices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Features.Users.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, User>
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(
            AppDbContext context,
            ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<TokenResponseDto?> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var user = await ValidateRefreshTokenAsync(command.userId, command.refreshToken);
            if(user is null)
            {
                return null;
            }

            return new TokenResponseDto
            {
                AccessToken = _tokenService.CreateToken(user)
            };
        }

        private async Task<User?> ValidateRefreshTokenAsync(int id, string refreshToken)
        {
            var user = await _context.Users
             .FirstOrDefaultAsync(u => u.Id == id);

            if(user is null || user.RefreshToken != refreshToken
                || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return null;
            }
            return user;
        }
    }
}
