using FootballScoreApp.Entities;

namespace FootballScoreApp.Repositories.IRepositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role?> GetRoleByRoleNameAsync(string roleName, CancellationToken cancellationToken);
        Task<Role?> GetOrCreateRoleByNameAsync(string roleName, CancellationToken cancellationToken);
    }
}
