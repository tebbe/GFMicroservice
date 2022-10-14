using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class ResponsibilityReAssignQuery:IRequest<IEnumerable<Dictionary<string,object>>>
    {
        public string AppKey { get; set; }
        public string Search { get; set; }
        public string UserType { get; set; }    
        public string Did { get; set; } 
        public Pagination Pagination { get; set; }  

    }
}
