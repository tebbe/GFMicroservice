using MediatR;
using Model.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Commands
{
    public class InsertRoleCommand : IRequest<string>
    {
        public string AppKey { get; set; }
        public RoleModel RoleModel { get; set; }
        
        public InsertRoleCommand(string userId, RoleModel model)
        {
            RoleModel = model;
            RoleModel.Did = Guid.NewGuid().ToString("N");
            RoleModel.CreatedBy = userId;
            RoleModel.InsertedDate = DateTime.UtcNow;
                  
        }
    
    }
}
