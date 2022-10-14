using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
  
    public class GetBuildingSensors: IRequest<IEnumerable<Dictionary<string, object>>>
    {
        public string BuildingID { get; set; }
        public string SensorType { get; set; }
        public string Token { get; set; }
       
    }
}
