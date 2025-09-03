using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using FootballScoreApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Repositories
{
    public class UserTeamRepository : Repository<UserTeam>, IUserTeamRepository
    {
        public UserTeamRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> UserIsFollowing(int teamId
            , int userId
            , CancellationToken cancellationToken
            )
        {
            return await _context.UserTeam
                .AnyAsync(ut => ut.TeamId == teamId && ut.UserId == userId
                , cancellationToken
                );
        }
        
        public async Task<bool> DeleteUserTeam(int teamId
            , int userId
            , CancellationToken cancellationToken
            )
        {
            var entityToRemove = await _context.UserTeam
                .SingleAsync(ut => ut.TeamId == teamId && ut.UserId == userId
                , cancellationToken
            );

            _context.UserTeam.Remove(entityToRemove);

            return true;
        }
    }
}
