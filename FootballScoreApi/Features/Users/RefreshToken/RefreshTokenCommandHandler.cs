using FootballScoreApp.DbConnection;
using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FootballScoreApp.Features.Users.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, User>
    {
        private readonly AppDbContext _context;

        public RefreshTokenCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TokenResponseDto?> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var user = await ValidateRefreshTokenAsync(command.userId, command.refreshToken);
            if(user is null)
            {
                return null;
            }
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
