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
    public class GetUserRolesListTotalByQuery : IRequest<long>
    {
        public string AppKey { get; set; }
        public RolelistQueryModel QueryParam { get; set; }
        public Pagination Paging { get; set; }
    }
}
