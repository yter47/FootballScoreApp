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

        public async Task<Role?> GetOrCreateRoleByNameAsync(string roleName, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
            if (role != null) return role;

            var newRole = new Role { Name = roleName };
            _context.Roles.Add(newRole);
            await _context.SaveChangesAsync(cancellationToken);

            return newRole;
        }

        public async Task<Role?> GetRoleByRoleNameAsync(string roleName, CancellationToken cancellationToken)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
        }
    }
}
