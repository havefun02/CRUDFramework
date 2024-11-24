using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDFramework.Cores
{
    public abstract class CreateRangeHandler<TResponse, TDBContext> : IRequestHandler<CreateRangeQuery<TResponse>, List<TResponse>>
        where TResponse : class 
        where TDBContext : DbContext
    {
        private readonly TDBContext _dbContext;

        protected CreateRangeHandler(TDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async virtual Task<List<TResponse>> Handle(CreateRangeQuery<TResponse> request, CancellationToken cancellationToken)
        {
            var entities = request.GetSources()?.ToList();
            if (entities == null || !entities.Any())
            {
                throw new NullReferenceException("Entities cannot be null or empty.");
            }

            var dbSet = _dbContext.Set<TResponse>();
            foreach (var entity in entities)
            {
                if (await dbSet.AnyAsync(e => e.Equals(entity), cancellationToken))
                {
                    throw new InvalidOperationException("Cannot add existing entity.");
                }
            }

            await _dbContext.AddRangeAsync(entities, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entities;
        }
    }
}
