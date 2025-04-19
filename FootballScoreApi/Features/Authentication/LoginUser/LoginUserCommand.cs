using FootballScoreApp.DTOs;
using MediatR;

namespace FootballScoreApp.Features.Authentication.LoginUser
{
    public record LoginUserCommand(string Username, string Password) : IRequest<TokenResponseDto>;
}
