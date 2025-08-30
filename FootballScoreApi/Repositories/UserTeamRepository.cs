using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using FootballScoreApp.Features.User.FollowTeam;
using FootballScoreApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Repositories
{
    public class UserTeamRepository : Repository<UserTeam>, IUserTeamRepository
    {
        public UserTeamRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> UserIsFollowing(FollowTeamCommand followTeamCommand
            , CancellationToken cancellationToken
            )
        {
            return await _context.UserTeam
                .AnyAsync(ut => ut.TeamId == followTeamCommand.teamId && ut.UserId == followTeamCommand.userId
                , cancellationToken
                );
        }
    }
}
