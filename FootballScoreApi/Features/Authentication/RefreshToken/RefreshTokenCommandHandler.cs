using FootballScoreApp.DTOs;
using FootballScoreApp.Entities;
using FootballScoreApp.Providers;
using FootballScoreApp.Repositories.IRepositories;
using MediatR;

namespace FootballScoreApp.Features.Authentication.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponseDto?>
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

        public async Task<TokenResponseDto?> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var user = await ValidateRefreshTokenAsync(command.RefreshToken, cancellationToken);
            if (user is null)
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

        private async Task<User?> ValidateRefreshTokenAsync(string token, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshTokenUserAndRolesByRefreshTokenAsync(token, cancellationToken);


            if (refreshToken is null || refreshToken.Token!= token
                || refreshToken.RefreshTokenExpiryTimeUtc < DateTime.UtcNow)
            {
                return null;
            }
            return refreshToken.User;
        }
    }
}
