using FootballScoreApp.DbConnection;
using FootballScoreApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken) => await _dbSet.ToListAsync();
        public void Add(T entity, CancellationToken cancellationToken) => _dbSet.Add(entity);
        public void Delete(T entity, CancellationToken cancellationToken) => _dbSet.Remove(entity);
        public Task SaveChangesAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync();
    }
}
