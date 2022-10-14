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
    public class GetAlertTotalCountHandler : IRequestHandler<GetAlertTotalCount, long>
    {
        private AlertsDal _osAlertDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetAlertTotalCountHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, AlertsDal osAlertDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _osAlertDal = osAlertDal;
        }
        public async Task<long> Handle(GetAlertTotalCount request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string osAlertCollectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.OsAlert);
                return await _osAlertDal.GetTotalCountAsync(osAlertCollectionName, request.QueryParam);
            }
            catch
            {
                throw;
            }
        }
    }
}
