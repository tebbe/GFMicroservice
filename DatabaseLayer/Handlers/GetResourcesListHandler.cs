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
    public class GetResourcesListHandler : IRequestHandler<GetResourcesList, IEnumerable<Dictionary<string, object>>>
    {
        private ResourcesDal _resourcesDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetResourcesListHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, ResourcesDal resourcesDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _resourcesDal = resourcesDal;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> Handle(GetResourcesList request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string CollectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.GFResources);
                return await _resourcesDal.GetAsync(CollectionName);
            }
            catch
            {
                throw;
            }
        }
    }
}
