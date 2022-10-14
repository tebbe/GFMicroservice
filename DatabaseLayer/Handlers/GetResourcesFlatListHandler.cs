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
    public class GetResourcesFlatListHandler : IRequestHandler<GetResourcesFlatList, IEnumerable<GFResourcesFlatModel>>
    {
        private ResourcesFlatDal _resourcesflatDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetResourcesFlatListHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, ResourcesFlatDal resourcesflatDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _resourcesflatDal = resourcesflatDal;
        }

        public async Task<IEnumerable<GFResourcesFlatModel>> Handle(GetResourcesFlatList request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string CollectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.GFResourcesFlat);
                return await _resourcesflatDal.GetAsync(CollectionName, request.FloorId);
            }
            catch
            {
                throw;
            }
        }
    }
}
