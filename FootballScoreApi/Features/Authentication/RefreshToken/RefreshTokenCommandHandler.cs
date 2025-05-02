using FootballScoreApp.Abstractions;
using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Providers;
using FootballScoreApp.Repositories.IRepositories;
using MediatR;

namespace FootballScoreApp.Features.Authentication.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<TokenResponseDto?>>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenProvider _tokenProvider;

        public RefreshTokenCommandHandler(
            IRefreshTokenRepository refreshTokenRepository,
            ITokenProvider tokenProvider)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _tokenProvider = tokenProvider;
        }

        public async Task<Result<TokenResponseDto?>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.RefreshToken))
            {
                return Result<TokenResponseDto?>.Failure("Refresh token is required.");
            }

            var result = await ValidateRefreshTokenAsync(command.RefreshToken, cancellationToken);
            if (result.IsFailure)
            {
                var errorMessage = result.Error ?? "Unknown error occurred while validating refresh token.";
                return Result<TokenResponseDto?>.Failure(errorMessage);
            }

            var user = result.Value;
            if (user is null)
            {
                return Result<TokenResponseDto?>.Failure("User not found associated with refresh token.");
            }

            var refreshToken = _tokenProvider.GenerateRefreshToken(user);
            _refreshTokenRepository.Add(refreshToken);
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

        private async Task<Result<User>> ValidateRefreshTokenAsync(string token, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshTokenUserAndRolesByRefreshTokenAsync(token, cancellationToken);

            if (refreshToken is null)
            {
                return Result<User>.Failure("Refresh token not found.");
            }

            if (refreshToken.Token != token)
            {
                return Result<User>.Failure("Token mismatch.");
            }

            if (refreshToken.RefreshTokenExpiryTimeUtc < DateTime.UtcNow)
            {
                return Result<User>.Failure("Refresh token has expired.");
            }

            return Result<User>.Success(refreshToken.User);
        }
    }
}
