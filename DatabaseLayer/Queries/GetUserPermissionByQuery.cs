using MediatR;
using Model;
using Model.QueryString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class GetUserPermissionByQuery : IRequest<IEnumerable<Dictionary<string, object>>>
    {
        public string AppKey { get; set; }
        public UserPermissionQueryModel QueryParam { get; set; }
        public Pagination Paging { get; set; }
    }
}
