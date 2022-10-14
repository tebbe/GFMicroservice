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
    internal class GetUserPermissionByDidHandler : IRequestHandler<GetUserPermissionByDid, Dictionary<string, object>>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private UserPermissionDal _userPermissionDal;
        public GetUserPermissionByDidHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, UserPermissionDal userPermissionDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userPermissionDal = userPermissionDal;
        }

        public async Task<Dictionary<string, object>> Handle(GetUserPermissionByDid request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.UserPermission);

                return await _userPermissionDal.GetByDidAsync(collectionName, request.Did);
            }
            catch
            {
                throw;
            }
        }
    }
}
