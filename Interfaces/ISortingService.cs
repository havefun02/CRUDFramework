using CRUDFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework
{
     public interface ISortingService<T> where T : class
    {
       IQueryable<T> Sort(IQueryable<T> query, SortingParams orderParams);

    }
}
