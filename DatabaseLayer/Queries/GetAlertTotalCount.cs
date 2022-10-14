﻿using MediatR;
using Model.QueryString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class GetAlertTotalCount : IRequest<long>
    {
        public string AppKey { get; set; }
        public AlertQueryModel QueryParam { get; set; }
    }
}
