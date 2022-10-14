using MediatR;
using Model;
using Model.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class GetActiveBuilding : IRequest<IEnumerable<Dictionary<string, object>>>
    {
        public string AppKey { get; set; }
        public Pagination Paiging { get; set; }
    }
}
