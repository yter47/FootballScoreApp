using FootballScoreApp.Entities;
using MediatR;

namespace FootballScoreApp.Features.Users.GetUserById
{
    public record GetUserByIdQuery(int Id) : IRequest<User?>;
}
