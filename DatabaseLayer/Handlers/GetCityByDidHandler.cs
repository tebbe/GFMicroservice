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
    public class GetCityByDidHandler : IRequestHandler<GetCityByUserID, Dictionary<string, object>>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private CitiesDal _citiesDal;

        public GetCityByDidHandler(CitiesDal citiesDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _citiesDal = citiesDal;
        }

        public async Task<Dictionary<string, object>> Handle(GetCityByUserID request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.Inventory_City);
                return await _citiesDal.GetCityByDidAsync(collectionName, request.CityID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
