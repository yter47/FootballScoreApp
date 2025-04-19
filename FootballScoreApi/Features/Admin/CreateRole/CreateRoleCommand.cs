using FootballScoreApp.Entities;
using MediatR;

namespace FootballScoreApp.Features.Roles.CreateRole
{
    public record CreateRoleCommand(string Name) : IRequest<Role>;
}
