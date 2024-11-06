using Microsoft.EntityFrameworkCore;

namespace CRUDFramework
{
     public  interface IRepository<T,TContext> where T : class where TContext : DbContext
    {
        public Task CreateAsync(T entity);
        public Task CreateRangeAsync(List<T> entity);
        public void Update(T entity);
        public void UpdateRange(List<T> entities);
        public Task  Delete(object PrimaryKey);
        public Task<T> FindOneById(object PrimaryKey);
        public Task<List<T>> FindAll();
        public TContext GetDbContext();
        public DbSet<T> GetDbSet();
        public Task SaveAsync();


    }
}
