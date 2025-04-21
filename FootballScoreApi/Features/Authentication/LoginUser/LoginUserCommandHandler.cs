using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Providers;
using FootballScoreApp.Repositories.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FootballScoreApp.Features.Authentication.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponseDto?>
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginUserCommandHandler(
            ITokenProvider tokenProvider,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _tokenProvider = tokenProvider;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<TokenResponseDto?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithRolesByUsernameAsync(request.Username, cancellationToken);

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
