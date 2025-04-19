using FootballScoreApp.DbConnection;
using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Services.IServices;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Features.Authentication.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponseDto?>
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(
            AppDbContext context,
            ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<TokenResponseDto?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

            if (user is null)
            {
                return null;
            }

            if (user.Username != request.Username)
            {
                return null;
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var refreshToken = _tokenService.GenerateRefreshToken(user);
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new TokenResponseDto
            {
                AccessToken = _tokenService.CreateToken(user),
                RefreshToken = refreshToken.Token
            };
        }
    }
}
