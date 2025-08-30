using FootballScoreApp.Entities;
using FootballScoreApp.Features.User.FollowTeam;

namespace FootballScoreApp.Repositories.IRepositories
{
    public interface IUserTeamRepository : IRepository<UserTeam>
    {
        Task<bool> UserIsFollowing(FollowTeamCommand followTeamCommand, CancellationToken cancellationToken);
    }
}
