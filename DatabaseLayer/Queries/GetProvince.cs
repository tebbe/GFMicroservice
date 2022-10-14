using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class GetProvince : IRequest<IEnumerable<Dictionary<string, object>>>
    {
        public string AppKey { get; set; }
        public string Province { get; set; }
        public Pagination Paiging { get; set; }
    }
}
