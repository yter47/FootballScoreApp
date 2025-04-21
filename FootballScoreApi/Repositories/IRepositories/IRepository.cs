namespace FootballScoreApp.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        void Add(T entity);
        void Delete(T entity);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
