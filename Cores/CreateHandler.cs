using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CRUDFramework.Cores
{
    abstract class CreateHandler< TResponse,TDBContext> : IRequestHandler<CreateQuery<TResponse>, TResponse> 
        where TResponse: class
        where TDBContext : DbContext
    {
        private readonly TDBContext _dbContext;
        public CreateHandler(TDBContext dbContext) { _dbContext = dbContext; }
        public async virtual Task<TResponse> Handle(CreateQuery<TResponse> request, CancellationToken cancellationToken)
        {
            var entity = request.GetSource();
            if (entity == null) {
                throw new NullReferenceException("Null Entity");
            }
            var isExisted = await this._dbContext.FindAsync<TResponse>(entity);
            if (isExisted != null) {
                throw new InvalidOperationException("Can not add existing entity");
            }
            _dbContext.Add<TResponse>(entity);
            await this._dbContext.SaveChangesAsync();
            var result= await this._dbContext.FindAsync<TResponse>(entity);
            if (result == null) throw new InternalServerException("Unknown exception");
            return result;
        }
    }
}
