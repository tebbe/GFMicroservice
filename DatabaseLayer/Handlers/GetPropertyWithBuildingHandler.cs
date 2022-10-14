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
    public class GetPropertyWithBuildingHandler : IRequestHandler<GetPropertyWithBuilding, IEnumerable<Dictionary<string, object>>>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private PropertiesDal _propertyDal;

        public GetPropertyWithBuildingHandler(PropertiesDal propertyDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _propertyDal = propertyDal;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> Handle(GetPropertyWithBuilding request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.Inventory_Property);
                string buildingCollectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.Inventory_Building);
                return await _propertyDal.GetPropertyWithBuilding(buildingCollectionName, collectionName, request.Name);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
