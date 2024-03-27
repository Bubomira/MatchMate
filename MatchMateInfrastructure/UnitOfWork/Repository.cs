using MatchMateInfrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace MatchMateInfrastructure.UnitOfWork
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task AddAsync<T>(T entity) where T : class
        {
           await DbSet<T>().AddAsync(entity);
        }

        public IQueryable<T> All<T>() where T : class => DbSet<T>();

        public IQueryable<T> AllReadOnly<T>() where T : class =>
            DbSet<T>().AsNoTracking();
     

        public async Task Remove<T>(T entity) where T : class
        {
            DbSet<T>().Remove(entity);
        }

        public async Task RemoveAll<T>(List<T> entities) where T : class
        {
            DbSet<T>().RemoveRange(entities);
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
       

        private DbSet<T> DbSet<T>() where T : class => _context.Set<T>();
    }
}
