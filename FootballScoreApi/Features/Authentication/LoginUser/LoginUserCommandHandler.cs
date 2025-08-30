using FootballScoreApp.Abstractions;
using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Providers;
using FootballScoreApp.Repositories.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FootballScoreApp.Features.Authentication.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<TokenResponseDto>>
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

        public async Task<Result<TokenResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithRolesByUsernameAsync(request.Username, cancellationToken);

            if (user is null)
                return Result<TokenResponseDto>.Failure("Invalid username or password");

            var passwordCheck = new PasswordHasher<Entities.User>().VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (passwordCheck == PasswordVerificationResult.Failed)
                return Result<TokenResponseDto>.Failure("Invalid username or password");

            var refreshToken = _tokenProvider.GenerateRefreshToken(user);
            _refreshTokenRepository.Add(refreshToken, cancellationToken);
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

            var tokenResponse = new TokenResponseDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                AccessToken = _tokenProvider.CreateToken(user),
                RefreshToken = refreshToken.Token
            };

            return Result<TokenResponseDto>.Success(tokenResponse);
        }
    }
}
