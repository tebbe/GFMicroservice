using DatabaseLayer.Dal;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.Extensions.Options;
using Model;
using PremiseGlobalLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseLayer.Handlers
{
    public class GetEODReportHandler : IRequestHandler<GetEODReport, IEnumerable<EODReport>>
    {
        private EODReportDal _eodReportDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetEODReportHandler(EODReportDal eodReportDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _eodReportDal = eodReportDal;
            _premiseService = premiseService;
            _dbCollections = dbCollections;
        }

        public async Task<IEnumerable<EODReport>> Handle(GetEODReport request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.EODReport);
                return await _eodReportDal.GetEODReportAsync(collectionName,request.QueryModel.BuildingId,request.QueryModel.Type,request.QueryModel.Others);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
