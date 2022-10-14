using MediatR;
using Model;
using Model.QueryString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class GetAlertByQuery : IRequest<IEnumerable<AlertModel>>
    {
        public string AppKey { get; set; }

        public AlertQueryModel QueryParam { get; set; }
        public Pagination Paging { get; set; }

    }
}
