using MediatR;
using Model.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Commands
{
    public class UpdateRoleCommand : IRequest<string>
    {
        public string AppKey { get; set; }
        
        public RoleModel RoleModel { get; set; }

        public UpdateRoleCommand(string userId, RoleModel model)
        {
            RoleModel = model; 
            RoleModel.UpdatedBy = userId;
            RoleModel.UpdatedDate = DateTime.UtcNow;


        }
    }
}
