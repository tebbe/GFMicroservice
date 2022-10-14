﻿using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
   public class GetUserBuildingList : IRequest<IEnumerable<GroundFloorBuildings>>
    {
        public string AppKey { get; set; }
        public string UserID { get; set; }

    }
}
