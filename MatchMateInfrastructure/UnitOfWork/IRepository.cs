namespace MatchMateInfrastructure.UnitOfWork
{
    public interface IRepository
    {
        public IQueryable<T> All<T>() where T : class;
        public IQueryable<T> AllReadOnly<T>() where T : class;
        public Task Add<T>(T entity) where T : class;
        public Task Remove<T>(T entity) where T : class;
        public Task<int> SaveChangesAsync();
    }
}
