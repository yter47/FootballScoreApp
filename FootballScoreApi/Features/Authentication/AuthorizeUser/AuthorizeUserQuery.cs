using FootballScoreApp.Abstractions;
using FootballScoreApp.DTOs;
using MediatR;

namespace FootballScoreApp.Features.Authentication.AuthorizeUser
{
    public record AuthorizeUserQuery(string Username) : IRequest<Result<TokenResponseDto>>;
}
