using FootballScoreApp.DbConnection;
using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Services.IServices;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Features.Authentication.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, TokenResponseDto?>
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public RegisterUserCommandHandler(
            AppDbContext context,
            ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<TokenResponseDto?> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == request.Username.ToLower(), cancellationToken))
            {
                return null;
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username
            };

            var passwordHash = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            user.PasswordHash = passwordHash;

            var defaultRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == "User", cancellationToken);

            if (defaultRole == null)
            {
                defaultRole = new Role { Name = "User" };
                _context.Roles.Add(defaultRole);
                await _context.SaveChangesAsync(cancellationToken);
            }

            user.UserRoles = new List<UserRole>
            {
                new UserRole { Role = defaultRole }
            };

            _context.Add(user);
            await _context.SaveChangesAsync();

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
