using FootballScoreApp.DbConnection;
using FootballScoreApp.Entities;
using FootballScoreApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace FootballScoreApp.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken?> GetRefreshTokenUserAndRolesByRefreshTokenAsync(string token, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens
                .Include(r => r.User)
                    .ThenInclude(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(r => r.Token == token, cancellationToken);
        }
    }
}
