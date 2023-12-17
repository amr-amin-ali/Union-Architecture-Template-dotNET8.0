namespace Elearning.Contracts.Repositories
{
    using System.Linq.Expressions;

    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> expression);
        Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(List<T> entities);
        Task<int> SaveAsync();
        int Save();
        Task Refresh(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> FindByConditionAsNoTracking(Expression<Func<T, bool>> expression);
        IQueryable<T> FindAllAsNoTracking();
        Task<T> GetLastAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> FindAllATracking();
        Task<IEnumerable<T>> FindAllAsync();
    }
}
