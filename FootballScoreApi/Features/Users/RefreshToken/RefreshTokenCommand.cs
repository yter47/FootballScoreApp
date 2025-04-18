using FootballScoreApp.DTOs;
using MediatR;

namespace FootballScoreApp.Features.Users.RefreshToken
{
    public record RefreshTokenCommand(int userId, string refreshToken) : IRequest<TokenResponseDto?>;
}
