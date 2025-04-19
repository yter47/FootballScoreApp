using FootballScoreApp.DTOs;
using MediatR;

namespace FootballScoreApp.Features.Users.RefreshToken
{
    public record RefreshTokenCommand(string refreshToken) : IRequest<TokenResponseDto?>;
}
