using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework.Cores
{
    public class CreateQuery<TResponse>:IRequest<TResponse>
    {
        private readonly TResponse _source; 
        public CreateQuery(TResponse source)
        {
            _source= source;  
        }
        public virtual TResponse GetSource()
        {
            return _source;
        }
    }
}
