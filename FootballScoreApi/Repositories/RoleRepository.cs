using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using FootballScoreApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Role?> GetRoleByRoleNameAsync(string roleName, CancellationToken cancellationToken)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
        }
    }
}
