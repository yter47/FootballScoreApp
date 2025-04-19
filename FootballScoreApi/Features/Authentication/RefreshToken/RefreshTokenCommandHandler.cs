using FootballScoreApp.DbConnection;
using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Services.IServices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Features.Authentication.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponseDto?>
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
            var user = await ValidateRefreshTokenAsync(command.RefreshToken, cancellationToken);
            if (user is null)
            {
                return null;
            }

            var refreshToken = _tokenService.GenerateRefreshToken(user);
            _context.Add(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new TokenResponseDto
            {
                AccessToken = _tokenService.CreateToken(user),
                RefreshToken = refreshToken.Token
            };
        }

        private async Task<User?> ValidateRefreshTokenAsync(string token, CancellationToken cancellationToken)
        {
            var refreshToken = await _context.RefreshTokens
                .Include(r => r.User)
                    .ThenInclude(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(r => r.Token == token, cancellationToken);


            if (refreshToken is null || refreshToken.Token!= token
                || refreshToken.RefreshTokenExpiryTimeUtc < DateTime.UtcNow)
            {
                return null;
            }
            return refreshToken.User;
        }
    }
}
