using CRUDFramework.Cores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework.Interfaces
{
     public interface IFilterService<T> where T : class
    {
        IQueryable<T> Filter(IQueryable<T> query, FilterParams<object> filterParams);

    }
}
