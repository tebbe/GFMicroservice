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
    public class GetProvinceByDidHandler : IRequestHandler<GetProvinceByUserID, Dictionary<string, object>>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private ProvincesDal _provinceDal;

        public GetProvinceByDidHandler(ProvincesDal provinceDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _provinceDal = provinceDal;
        }

        public async Task<Dictionary<string, object>> Handle(GetProvinceByUserID request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.Inventory_Province);
                return await _provinceDal.GetProvinceByDidAsync(collectionName, request.ProvinceID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
