using MediatR;
using Model.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Commands
{
    public class InsertRolePermissionMappingCommand : IRequest<bool>
    {
        public string AppKey { get; set; }
        public RolePermissionMappingModel permissionModel { get; set; }

        public InsertRolePermissionMappingCommand(string userId, RolePermissionMappingModel model)
        {
            permissionModel = model;
            permissionModel.Did = Guid.NewGuid().ToString("N");
            permissionModel.CreatedBy = userId;
            permissionModel.InsertedDate = DateTime.UtcNow;

        }


    }
}
