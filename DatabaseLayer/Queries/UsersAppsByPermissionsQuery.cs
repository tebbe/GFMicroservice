﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class UsersAppsByPermissionsQuery:IRequest<bool>
    {
        public string AppKey { get; set; }
        public string UserId { get; set; }  
        public string[] RoleDid { get; set; } 
        public string[] UserPermission { get; set; }    
    }
}
