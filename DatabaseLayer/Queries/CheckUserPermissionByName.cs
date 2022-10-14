﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class CheckUserPermissionByName : IRequest<bool>
    {
        public string Appkey { get; set; }
        public string Did { get; set; }
        public string PermissionName { get; set; }
        public string PermissionKey { get; set; }
    }
}
