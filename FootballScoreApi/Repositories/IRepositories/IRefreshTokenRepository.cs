using FootballScoreApp.Entities;

namespace FootballScoreApp.Repositories.IRepositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken?> GetRefreshTokenUserAndRolesByRefreshTokenAsync(string token, CancellationToken cancellationToken);
    }
}
