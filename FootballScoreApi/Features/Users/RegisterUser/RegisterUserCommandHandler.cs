using FootballScoreApp.DbConnection;
using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Services.IServices;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Features.Users.RegisterUser
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

            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == request.username.ToLower()))
            {
                return null;
            }

            var user = new User
            {
                FirstName = request.firstName,
                LastName = request.lastName,
                Username = request.username
            };

            var passwordHash = new PasswordHasher<User>()
                .HashPassword(user, request.password);

            user.PasswordHash = passwordHash;

            var defaultRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == "User");

            if (defaultRole == null)
            {
                defaultRole = new Role { Name = "User" };
                await _context.Roles.AddAsync(defaultRole);
                await _context.SaveChangesAsync();
            }

            user.UserRoles = new List<UserRole>
            {
                new UserRole { Role = defaultRole }
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            var refreshToken = _tokenService.GenerateRefreshToken(user);
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new TokenResponseDto
            {
                AccessToken = _tokenService.CreateToken(user),
                RefreshToken = refreshToken.Token
            };
        }
    }
}
