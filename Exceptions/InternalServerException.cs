using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework
{
    class InternalServerException:Exception
    {
        public InternalServerException(string message, Exception innerException)
                    : base(message, innerException)
        {
        }
        public InternalServerException(string message)
                    : base(message)
        {
        }
    }
}
