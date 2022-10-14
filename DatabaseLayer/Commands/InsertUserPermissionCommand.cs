using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Commands
{
    public class InsertUserPermissionCommand : IRequest<string>
    {
        public string AppKey { get; set; }
        public UserPermissionModel userPermissionModel { get; set; }
        public InsertUserPermissionCommand(string userId, UserPermissionModel model)
        {
            userPermissionModel = model;
            userPermissionModel.Did = Guid.NewGuid().ToString("N");
            userPermissionModel.CreatedBy = userId;
            userPermissionModel.InsertedDate = DateTime.UtcNow;
        }
    }
}
