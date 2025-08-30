using FootballScoreApp.Abstractions;
using MediatR;

namespace FootballScoreApp.Features.User.FollowTeam
{
    public record FollowTeamCommand(int userId, int teamId) : IRequest<Result<int?>>;
}
