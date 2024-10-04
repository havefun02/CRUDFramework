using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRUDFramework.Interfaces;
using CRUDFramework.Exceptions;

namespace CRUDFramework.Repositories
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet=context.Set<T>();
        }
        public  DbSet<T> GetDbSet()
        {
            return _context.Set<T>();
        }
        public DbContext GetDbContext()
        {
            return _context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> CreateRangeAsync(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(T));
            }
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<T> Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(T));
            }
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<List<T>> UpdateRange(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(T));
            }
            var _dbSet = this.GetDbSet();
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<int> Delete(object primaryKey)
        {
            if (primaryKey == null)
            {
                throw new ArgumentNullException(nameof(primaryKey));
            }
            var entity = await _dbSet.FindAsync(primaryKey);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<T> FindOneById(object primaryKey)
        {
            if (primaryKey == null)
            {
                throw new ArgumentNullException(nameof(primaryKey));
            }
            var entity = await _dbSet.FindAsync(primaryKey);
            if (entity != null)
            {
                return entity;
            }
            else throw new NotFoundException();
        }

        public async Task<List<T>> FindAll()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
