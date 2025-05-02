using FootballScoreApp.Abstractions;
using FootballScoreApp.DTOs;
using FootballScoreApp.Providers;
using FootballScoreApp.Repositories.IRepositories;
using MediatR;

namespace FootballScoreApp.Features.Authentication.AuthorizeUser
{
    public class AuthorizeUserQueryHandler : IRequestHandler<AuthorizeUserQuery, Result<TokenResponseDto>>
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;


        public AuthorizeUserQueryHandler(
            ITokenProvider tokenProvider, 
            IUserRepository userRepository, 
            IRefreshTokenRepository refreshTokenRepository)
        {
            _tokenProvider = tokenProvider;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Result<TokenResponseDto>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithRolesByUsernameAsync(request.Username, cancellationToken);

            if (user is null)
                return Result<TokenResponseDto>.Failure("User was not found");

            var accessToken = _tokenProvider.CreateToken(user);

            var refreshToken = _tokenProvider.GenerateRefreshToken(user);
            _refreshTokenRepository.Add(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

            return Result<TokenResponseDto>.Success(new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            });
        }
    }
}
