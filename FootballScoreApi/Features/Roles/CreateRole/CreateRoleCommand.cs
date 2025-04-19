using FootballScoreApp.Entities;
using MediatR;

namespace FootballScoreApp.Features.Roles.CreateRole
{
    public record CreateRoleCommand(string name) : IRequest<Role>;
}
