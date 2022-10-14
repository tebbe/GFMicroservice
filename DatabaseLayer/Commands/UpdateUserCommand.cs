﻿using MediatR;
using Model.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Commands
{
    public class UpdateUserCommand:IRequest<bool>
    {
        public string AppKey { get; set; }
        public UserModel UserModel { get; set; }
        public UpdateUserCommand(string userId, UserModel model)
        {
            UserModel = model;
            UserModel.UpdatedBy = userId;
            UserModel.UpdatedDate = DateTime.UtcNow;
        }
    }
}