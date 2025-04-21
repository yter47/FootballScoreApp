using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using FootballScoreApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AnyAsync(u => u.Username.ToLower() == username.ToLower(), cancellationToken);

        }

        public async Task<User?> GetUserWithRolesByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower(), cancellationToken);
        }
    }
}
