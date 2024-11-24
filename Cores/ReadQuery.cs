using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework.Cores
{
    public class ReadQuery<TResponse>:IRequest<TResponse>
        where TResponse : class
    {

    }
}
