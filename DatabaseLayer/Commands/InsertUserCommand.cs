using MediatR;
using Model.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Commands
{
    public class InsertUserCommand:IRequest<string>
    {
        public string AppKey { get; set; }
        public UserModel UserModel { get; set; }
        public InsertUserCommand(string userId, UserModel model)
        {
            UserModel = model;
            UserModel.Did = Guid.NewGuid().ToString("N");
            UserModel.CreatedBy = userId;
            UserModel.InsertedDate = DateTime.UtcNow;
        }
    }
}
