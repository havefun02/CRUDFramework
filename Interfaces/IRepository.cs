using Microsoft.EntityFrameworkCore;

namespace CRUDFramework.Interfaces
{
    internal interface IRepository<T> where T : class
    {
        public Task<T> CreateAsync(T entity);
        public Task<List<T>> CreateRangeAsync(List<T> entity);
        public Task<T> Update(T entity);
        public Task<List<T>> UpdateRange(List<T> entities);

        public Task<int> Delete(object PrimaryKey);
        public Task<T> FindOneById(object PrimaryKey);
        public Task<List<T>> FindAll();
        public DbContext GetDbContext();
        public DbSet<T> GetDbSet();

    }
}
