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
    public class GetUserPermissionByQueryHandler : IRequestHandler<GetUserPermissionByQuery, IEnumerable<Dictionary<string, object>>>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private UserPermissionDal _userPermissionDal;
        public GetUserPermissionByQueryHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, UserPermissionDal userPermissionDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userPermissionDal = userPermissionDal;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> Handle(GetUserPermissionByQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.UserPermission);

                return await _userPermissionDal.GetAsync(collectionName, request.QueryParam, request.Paging);
            }
            catch
            {
                throw;
            }
        }
    }
}
