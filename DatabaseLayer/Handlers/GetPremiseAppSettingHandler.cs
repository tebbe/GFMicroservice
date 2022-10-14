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
     class GetPremiseAppSettingHandler : IRequestHandler<GetAppSettings, PremiseAppSettingsModel>
    {
        private AppSettingsDal _appSettingsDal;
        private  IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetPremiseAppSettingHandler(AppSettingsDal appSettingsDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _appSettingsDal = appSettingsDal;
            _premiseService = premiseService;
            _dbCollections = dbCollections;
        }

        public async Task<PremiseAppSettingsModel> Handle(GetAppSettings request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.PremiseAppSettings);
                return await _appSettingsDal.GetAsync(collectionName, request.AppID, request.SettingKey);

            }
            catch
            {
                throw;
            }

        }
    }
}
