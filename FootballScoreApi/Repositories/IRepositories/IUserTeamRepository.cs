using FootballScoreApp.Entities;
using FootballScoreApp.Features.User.FollowTeam;

namespace FootballScoreApp.Repositories.IRepositories
{
    public interface IUserTeamRepository : IRepository<UserTeam>
    {
        Task<bool> UserIsFollowing(int userId, int teamId, CancellationToken cancellationToken);
        Task<bool> DeleteUserTeam(int teamId, int userId, CancellationToken cancellationToken);
    }
}
