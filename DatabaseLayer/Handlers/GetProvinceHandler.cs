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
    public class GetProvinceHandler : IRequestHandler<GetProvince, IEnumerable<Dictionary<string, object>>>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private ProvincesDal _provinceDal;

        public GetProvinceHandler(ProvincesDal provinceDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _provinceDal = provinceDal;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> Handle(GetProvince request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.Inventory_Province);
                return await _provinceDal.GetProvince(collectionName, request.Province, request.Paiging);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
