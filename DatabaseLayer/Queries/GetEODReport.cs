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
    public class GetEODReport: IRequest<IEnumerable<EODReport>>
    {
        public string AppKey { get; set; }
        public EODReportQueryModel QueryModel { get; set; } 
    }
}
