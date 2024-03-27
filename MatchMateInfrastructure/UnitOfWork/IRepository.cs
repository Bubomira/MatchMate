namespace MatchMateInfrastructure.UnitOfWork
{
    public interface IRepository
    {
        public IQueryable<T> All<T>() where T : class;
        public IQueryable<T> AllReadOnly<T>() where T : class;
        public Task AddAsync<T>(T entity) where T : class;
        public Task Remove<T>(T entity) where T : class;
        public Task RemoveAll<T>(List<T> entities) where T : class;
        public Task<int> SaveChangesAsync();
    }
}
