using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Commands
{
    public class DeleteRoleUserMappingCommand : IRequest<bool>
    {
        public string AppKey { get; set; }
        public string UserID { get; set; }
    }
}
