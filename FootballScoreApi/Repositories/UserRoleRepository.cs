using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using FootballScoreApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<UserRole?> GetUserRoleByUserAndRoleId(int userId, int roleId)
        {
            return await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }
    }
}
