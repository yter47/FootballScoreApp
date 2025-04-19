using FootballScoreApp.Entities;
using MediatR;

namespace FootballScoreApp.Features.Admin.AssignRoleToUser
{
    public record AssignRoleCommand(int UserId, int RoleId) : IRequest<UserRole?>;
}
