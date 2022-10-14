using MediatR;
using Model.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class GetPermissionByRoleId:IRequest<Dictionary<string, object>>
    {
        public string AppKey { get; set; }
        public string RoleId { get; set; }
    }
}
