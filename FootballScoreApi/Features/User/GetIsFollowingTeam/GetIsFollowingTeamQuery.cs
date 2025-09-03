using FootballScoreApp.Abstractions;
using MediatR;

namespace FootballScoreApp.Features.User.GetIsFollowingTeam
{
    public record GetIsFollowingTeamQuery(int teamId) : IRequest<Result<bool?>>;
}
