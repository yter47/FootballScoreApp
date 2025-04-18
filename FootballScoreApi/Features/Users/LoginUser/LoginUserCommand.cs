using FootballScoreApp.DTOs;
using MediatR;

namespace FootballScoreApp.Features.Users.LoginUser
{
    public record LoginUserCommand(string username, string password) : IRequest<TokenResponseDto>;
}
