namespace Elearning.Repositories
{
    using Microsoft.EntityFrameworkCore;

    using System.Linq.Expressions;

    using Elearning.Contracts.Repositories;
    using Elearning.Entittes.DbContexts;

    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected ElearningContext ElearningContext { get; set; }

        public Repository(ElearningContext elearningContext)
        {
            this.ElearningContext = elearningContext;
        }

        public IEnumerable<T> FindAll()
        {
            return ElearningContext.Set<T>();
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await ElearningContext.Set<T>().ToListAsync();
        }
        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return ElearningContext.Set<T>().Where(expression);
        }
        public async Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            var t = await ElearningContext.Set<T>().Where(expression).ToListAsync();
            return t;
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await ElearningContext.Set<T>().AnyAsync(expression);
        }

        public async Task<bool> AnyAsync()
        {
            return await ElearningContext.Set<T>().AnyAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await ElearningContext.Set<T>().CountAsync(expression);
        }
        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> expression)
        {
            return await ElearningContext.Set<T>().FirstOrDefaultAsync(expression);
        }
        public async Task<T> GetLastAsync(Expression<Func<T, bool>> expression)
        {
            return await ElearningContext.Set<T>().LastOrDefaultAsync(expression);
        }
        public async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await ElearningContext.Set<T>().SingleOrDefaultAsync(expression);
        }

        public IQueryable<T> FindByConditionAsNoTracking(Expression<Func<T, bool>> expression)
        {
            return ElearningContext.Set<T>().Where(expression).AsNoTracking();
        }

        public IQueryable<T> FindAllAsNoTracking()
        {
            return ElearningContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindAllATracking()
        {
            return ElearningContext.Set<T>().AsQueryable<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await ElearningContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            ElearningContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            ElearningContext.Set<T>().Remove(entity);
        }

        public void DeleteRange(List<T> entities)
        {
            ElearningContext.Set<T>().RemoveRange(entities);
        }
        public async Task<int> SaveAsync()
        {
            return await ElearningContext.SaveChangesAsync();
        }
        public int Save()
        {
            return ElearningContext.SaveChanges();
        }

        public async Task Refresh(T entity)
        {
            await ElearningContext.Entry(entity).ReloadAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
