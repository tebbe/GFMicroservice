﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class GetPMDemoScheduleList : IRequest<IEnumerable<Dictionary<string, object>>>
    {
        public string AppKey { get; set; }
    }
}
