using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class CheckUserByQuery:IRequest<bool>  
    {
        public string Appkey { get; set; }
        public string Did { get; set; }
        public string UserName { get; set; }
    }
}
