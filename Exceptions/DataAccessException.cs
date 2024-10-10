using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework.Exceptions
{
    class DataAccessException:Exception
    {
        public DataAccessException(string message, Exception innerException)
                    : base(message, innerException)
        {
        }
    }
}
