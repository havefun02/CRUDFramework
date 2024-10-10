﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRUDFramework.Interfaces;
using CRUDFramework.Exceptions;
using System;

namespace CRUDFramework.Cores
{
    /// <summary>
    /// Generic repository class for handling CRUD operations on a specified entity.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <typeparam name="TContext">The database context type.</typeparam>
    public class Repository<T, TContext> : IRepository<T, TContext>
        where T : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T, TContext}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public Repository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Gets the <see cref="DbSet{TEntity}"/> for the entity type.
        /// </summary>
        /// <returns>The <see cref="DbSet{T}"/>.</returns>
        public DbSet<T> GetDbSet()
        {
            return _context.Set<T>();
        }

        /// <summary>
        /// Gets the current database context.
        /// </summary>
        /// <returns>The database context of type <typeparamref name="TContext"/>.</returns>
        public TContext GetDbContext()
        {
            return _context;
        }

        /// <summary>
        /// Asynchronously creates a new entity in the database.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The created entity.</returns>
        /// <exception cref="DataAccessException">Thrown when there is an error during the create operation.</exception>
        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("An error occurred while creating the entity.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new DataAccessException("Invalid operation encountered while adding the entity.", ex);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously creates a list of entities in the database.
        /// </summary>
        /// <param name="entities">The list of entities to create.</param>
        /// <returns>The list of created entities.</returns>
        /// <exception cref="DataAccessException">Thrown when there is an error during the create range operation.</exception>
        public async Task<List<T>> CreateRangeAsync(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            try
            {
                await _dbSet.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                return entities;
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("An error occurred while creating the entity range.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new DataAccessException("Invalid operation encountered while adding the entity range.", ex);
            }
        }

        /// <summary>
        /// Asynchronously updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The updated entity.</returns>
        /// <exception cref="DataAccessException">Thrown when there is an error during the update operation.</exception>
        public async Task<T> Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("An error occurred while updating the entity.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new DataAccessException("Invalid operation encountered while updating the entity.", ex);
            }
        }

        /// <summary>
        /// Asynchronously updates a list of entities in the database.
        /// </summary>
        /// <param name="entities">The list of entities to update.</param>
        /// <returns>The updated list of entities.</returns>
        /// <exception cref="DataAccessException">Thrown when there is an error during the update range operation.</exception>
        public async Task<List<T>> UpdateRange(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            try
            {
                _dbSet.UpdateRange(entities);
                await _context.SaveChangesAsync();
                return entities;
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("An error occurred while updating the entity range.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new DataAccessException("Invalid operation encountered while updating the entity range.", ex);
            }
        }

        /// <summary>
        /// Asynchronously deletes an entity by its primary key.
        /// </summary>
        /// <param name="primaryKey">The primary key of the entity to delete.</param>
        /// <returns>The number of affected rows.</returns>
        /// <exception cref="DataAccessException">Thrown when there is an error during the delete operation.</exception>
        public async Task<int> Delete(object primaryKey)
        {
            if (primaryKey == null)
            {
                throw new ArgumentNullException(nameof(primaryKey));
            }

            try
            {
                var entity = await _dbSet.FindAsync(primaryKey);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    return await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("An error occurred while deleting the entity.", ex);
            }
        }

        /// <summary>
        /// Asynchronously finds an entity by its primary key.
        /// </summary>
        /// <param name="primaryKey">The primary key of the entity to find.</param>
        /// <returns>The entity if found.</returns>
        /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
        public async Task<T> FindOneById(object primaryKey)
        {
            if (primaryKey == null)
            {
                throw new ArgumentNullException(nameof(primaryKey));
            }

            try
            {
                var entity = await _dbSet.FindAsync(primaryKey);
                if (entity != null)
                {
                    return entity;
                }
                throw new NotFoundException();
            }
            catch (InvalidOperationException ex)
            {
                throw new DataAccessException("An error occurred while finding the entity by ID.", ex);
            }
        }

        /// <summary>
        /// Asynchronously retrieves all entities from the database.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        /// <exception cref="DataAccessException">Thrown when there is an error during the retrieval operation.</exception>
        public async Task<List<T>> FindAll()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new DataAccessException("An error occurred while retrieving all entities.", ex);
            }
        }
    }
}
