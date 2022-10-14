using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class GetUserDepartmentList : IRequest<IEnumerable<UserDepartmentModel>>
    {
        public string AppKey { get; set; }
        public Pagination Pagging { get; set; }
        
    }
}
