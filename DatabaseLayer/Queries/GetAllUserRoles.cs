using MediatR;
using Model;
using Model.QueryString;
using Model.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class GetAllUserRoles : IRequest<IEnumerable < Dictionary<string, object>>>
    {
        public string AppKey { get; set; }
        public RolelistQueryModel QueryModel { get; set; }
        public Pagination Paging { get; set; }

    }
}
