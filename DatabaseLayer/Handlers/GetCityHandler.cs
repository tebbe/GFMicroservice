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
    public class GetCityHandler : IRequestHandler<GetCity, IEnumerable<Dictionary<string, object>>>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private CitiesDal _citiesDal;

        public GetCityHandler(CitiesDal citiesDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _citiesDal = citiesDal;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> Handle(GetCity request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.Inventory_City);
                return await _citiesDal.GetCity(collectionName, request.ProvinceID, request.City, request.Paiging);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
