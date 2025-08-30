using FootballScoreApp.Abstractions;
using MediatR;

namespace FootballScoreApp.Features.Users.GetUserById
{
    public record GetUserByIdQuery(int Id) : IRequest<Result<Entities.User?>>;
}
