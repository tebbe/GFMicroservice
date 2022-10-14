﻿using MediatR;
using Model.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Commands
{
    public class UpdateRolePermissionMappingCommand : IRequest<bool>
    {
        public string AppKey { get; set; }
        public RolePermissionMappingModel permissionModel { get; set; } 
        
        public UpdateRolePermissionMappingCommand(string userId, RolePermissionMappingModel model)
        {
            permissionModel = model;
            permissionModel.UpdatedBy = userId;
            permissionModel.UpdatedDate = DateTime.UtcNow;
                  
        }
    
    }
}
