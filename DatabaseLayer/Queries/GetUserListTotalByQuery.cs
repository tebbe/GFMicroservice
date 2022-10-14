using MediatR;
using Model;
using Model.QueryString;
using Model.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class GetUserListTotalByQuery : IRequest<long>   
    {
        public string AppKey { get; set; }

        public UserListQueryModel QueryParam { get; set; }
        public Pagination Paging { get; set; }

    }
}
