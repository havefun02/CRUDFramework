using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework.Cores
{
    public abstract class ReadHandler<TResponse, TDBContext> : IRequestHandler<ReadQuery<IEnumerable<TResponse>>, IEnumerable<TResponse>>
        where
        TResponse : class
        where TDBContext : DbContext
    {
        private readonly TDBContext _dbContext;
        public ReadHandler(TDBContext dBContext) { _dbContext = dBContext; }
        public virtual async Task<IEnumerable<TResponse>> Handle(ReadQuery<IEnumerable<TResponse>> request, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TResponse>().AsNoTracking().ToListAsync();
        }
    }
}
