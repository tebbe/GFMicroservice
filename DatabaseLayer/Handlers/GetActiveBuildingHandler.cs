using DatabaseLayer.Dal;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.Extensions.Options;
using Model;
using Model.UserSystem;
using PremiseGlobalLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseLayer.Handlers
{
    public class GetActiveBuildingHandler : IRequestHandler<GetActiveBuilding, IEnumerable<Dictionary<string, object>>>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private BuildingsDal _buildingDal;

        public GetActiveBuildingHandler(BuildingsDal buildingDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _buildingDal = buildingDal;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> Handle(GetActiveBuilding request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.Inventory_Building);
                return await _buildingDal.GetActiveBuilding(collectionName, request.Paiging);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
