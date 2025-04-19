using FootballScoreApp.DTOs;
using MediatR;

namespace FootballScoreApp.Features.Authentication.RefreshToken
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<TokenResponseDto?>;
}
