using FootballScoreApp.Abstractions;
using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Providers;
using FootballScoreApp.Repositories.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FootballScoreApp.Features.Authentication.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<TokenResponseDto?>>
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

        public async Task<Result<TokenResponseDto?>> Handle(
            RegisterUserCommand request, 
            CancellationToken cancellationToken)
        {
            var user = new Entities.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username
            };

            var existingUser = await _userRepository.IsUsernameUniqueAsync(user.Username, cancellationToken);

            if (existingUser)
            {
                return Result<TokenResponseDto?>.Failure("Username already exists");
            }

            var passwordHash = new PasswordHasher<Entities.User>()
                .HashPassword(user, request.Password);

            user.PasswordHash = passwordHash;

            var defaultRole = await _roleRepository.GetOrCreateRoleByNameAsync("User", cancellationToken);

            if (defaultRole is null)
            {
                return Result<TokenResponseDto?>.Failure("Failed to get or create default 'User' role.");
            }

            user.UserRoles = new List<UserRole>
            {
                new UserRole { Role = defaultRole }
            };

            _userRepository.Add(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            var refreshToken = _tokenProvider.GenerateRefreshToken(user);
            _refreshTokenRepository.Add(refreshToken, cancellationToken);
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

            return Result<TokenResponseDto?>.Success(new TokenResponseDto  
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                AccessToken = _tokenProvider.CreateToken(user),
                RefreshToken = refreshToken.Token
            });
        }
    }
}
