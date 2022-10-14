using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Commands
{
    public class UpdateUserPermissionCommand : IRequest<string>
    {
        public UserPermissionModel userPermissionModel { get; set; }
        public string AppKey { get; set; }
        public string Did { get; set; }

        public UpdateUserPermissionCommand(string userID, UserPermissionModel model)
        {
            userPermissionModel = model;
            userPermissionModel.UpdatedDate = DateTime.UtcNow;
            userPermissionModel.UpdatedBy = userID;

        }
    }
}
