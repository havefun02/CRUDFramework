using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework
{
     public interface IPaginationParams
    {
        int limit { get; set; }
    }
}
