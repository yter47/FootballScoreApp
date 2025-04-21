using FootballScoreApp.Entities;

namespace FootballScoreApp.Repositories.IRepositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<UserRole?> GetUserRoleByUserAndRoleId(int userId, int roleId);
    }
}
