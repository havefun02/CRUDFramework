using CRUDFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework.Cores
{
     class OffsetPaginationParams : IPaginationParams
    {
        public int limit { get; set; } = 12;
        public int offset { get; set; } = 0;
    }
}
