using FootballScoreApp.Entities;
using MediatR;

namespace FootballScoreApp.Features.Admin.GetAllUsers
{
    public record GetAllUsersQuery : IRequest<IEnumerable<Entities.User>>;
}
