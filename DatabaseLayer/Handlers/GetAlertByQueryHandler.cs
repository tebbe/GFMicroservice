using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DatabaseLayer.Dal;
using DatabaseLayer.DBContext;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.Extensions.Options;
using Model;
using PremiseGlobalLibrary;

namespace DatabaseLayer.Handlers
{
    class GetAlertByQueryHandler : IRequestHandler<GetAlertByQuery, IEnumerable<AlertModel>>
    {
        private AlertsDal _osAlertDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetAlertByQueryHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, AlertsDal osAlertDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _osAlertDal = osAlertDal;
        }
        public async Task<IEnumerable<AlertModel>> Handle(GetAlertByQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string osAlertCollectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.OsAlert);
                return await _osAlertDal.GetAsync(osAlertCollectionName,request.QueryParam,request.Paging);
            }
            catch
            {
                throw;
            }
        }
    }
}
