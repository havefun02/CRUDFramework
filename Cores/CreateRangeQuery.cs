using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework.Cores
{
    public class CreateRangeQuery<TResponse> : IRequest<List<TResponse>>
    {
        private readonly IEnumerable<TResponse> _sources;
        public CreateRangeQuery(IEnumerable<TResponse> sources)
        {
            _sources = sources;
        }
        public virtual IEnumerable<TResponse> GetSources()
        {
            return _sources;
        }
    }
}
