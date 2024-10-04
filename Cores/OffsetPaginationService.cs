using CRUDFramework.Exceptions;
using CRUDFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework.Cores
{
    internal class OffsetPaginationService<T> : IPaginationService<T> where T : class
    {
        public async Task<IPaginationResult<T>> Paginate(IQueryable<T> query, IPaginationParams pageParams)
        {
            //Cast params
            var offsetParams = pageParams as OffsetPaginationParams;
            if (offsetParams == null) throw new ArgumentException("Invalid pagination parameters.");


            // Validate offset
            if (offsetParams.offset < 0) throw new ArgumentException("Offset cannot be negative.");

            var count = await query.CountAsync();
            var items = await query.Skip(offsetParams.offset)
                                   .Take(offsetParams.limit)
                                   .ToListAsync();

            //Cast result
            var paginationResult =
                new OffsetPaginationResult<T>
                {
                    totalItems = count,
                    limit = offsetParams.limit,
                    offset = offsetParams.offset,
                    items = items,
                };
            if (paginationResult == null) { throw new NotFoundException(); }
            return paginationResult;
        }
    }
}
