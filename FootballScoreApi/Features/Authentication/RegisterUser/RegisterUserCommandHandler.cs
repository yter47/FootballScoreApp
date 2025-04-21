using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Providers;
using FootballScoreApp.Repositories.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FootballScoreApp.Features.Authentication.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, TokenResponseDto?>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITokenProvider _tokenProvider;

        public RegisterUserCommandHandler(
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            ITokenProvider tokenProvider)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _tokenProvider = tokenProvider;
        }

        public async Task<TokenResponseDto?> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username
            };

            var passwordHash = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            user.PasswordHash = passwordHash;

            var defaultRole = await _roleRepository
                .GetRoleByRoleNameAsync("User", cancellationToken);

            if (defaultRole == null)
            {
                defaultRole = new Role { Name = "User" };
                _roleRepository.Add(defaultRole);
                await _roleRepository.SaveChangesAsync(cancellationToken);
            }

            user.UserRoles = new List<UserRole>
            {
                new UserRole { Role = defaultRole }
            };

            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync(cancellationToken);

            var refreshToken = _tokenProvider.GenerateRefreshToken(user);
            _refreshTokenRepository.Add(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

            return new TokenResponseDto
            {
                AccessToken = _tokenProvider.CreateToken(user),
                RefreshToken = refreshToken.Token
            };
        }
    }
}
