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
    public class GetEODRunningHandler : IRequestHandler<GetEODRunning, EODRunning>
    {
        private EODRunningDal _eodRunningDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetEODRunningHandler(EODRunningDal eodRunningDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _eodRunningDal = eodRunningDal;
            _premiseService = premiseService;
            _dbCollections = dbCollections;
        }

        public async Task<EODRunning> Handle(GetEODRunning request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.EODRunning);
                return await _eodRunningDal.GetEODAsync(collectionName, request.BuildingID,request.CurrentMonthDate);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
