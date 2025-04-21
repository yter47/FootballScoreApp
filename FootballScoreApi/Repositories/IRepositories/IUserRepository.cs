using FootballScoreApp.Entities;

namespace FootballScoreApp.Repositories.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken);
        Task<User?> GetUserWithRolesByUsernameAsync(string username, CancellationToken cancellationToken);
    }
}
