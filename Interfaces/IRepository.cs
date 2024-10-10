using Microsoft.EntityFrameworkCore;

namespace CRUDFramework.Interfaces
{
     public  interface IRepository<T,TContext> where T : class where TContext : DbContext
    {
        public Task<T> CreateAsync(T entity);
        public Task<List<T>> CreateRangeAsync(List<T> entity);
        public Task<T> Update(T entity);
        public Task<List<T>> UpdateRange(List<T> entities);
        public Task<int> Delete(object PrimaryKey);
        public Task<T> FindOneById(object PrimaryKey);
        public Task<List<T>> FindAll();
        public TContext GetDbContext();
        public DbSet<T> GetDbSet();

    }
}
