using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework.Interfaces
{
    internal interface IPaginationService<T> where T : class
    {
         Task<IPaginationResult<T>> Paginate(IQueryable<T> query, IPaginationParams pageParams);
    }
}
