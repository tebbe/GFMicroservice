﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class GetUserPermissionByDid : IRequest<Dictionary<string, object>>
    {
        public string AppKey { get; set; }
        public string Did { get; set; }
    }
}
