using MediatR;
using Model.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Commands
{
    public class InsertRoleUserMappingCommand : IRequest<bool>
    {
        public RoleUserMapping RoleUserMappingObj { get; set; }
        public string AppKey { get; set; }
        public InsertRoleUserMappingCommand(string UserID, RoleUserMapping roleUserMapping)
        {
            RoleUserMappingObj = roleUserMapping;
            RoleUserMappingObj.Did = Guid.NewGuid().ToString("N");
            RoleUserMappingObj.InsertedDate = DateTime.UtcNow;
            RoleUserMappingObj.UpdatedDate = DateTime.UtcNow;
            RoleUserMappingObj.CreatedBy = UserID;
            RoleUserMappingObj.UpdatedBy = UserID;
        }
    }
}
