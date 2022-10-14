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
    public class GetUserRolesListTotalHandler : IRequestHandler<GetUserRolesListTotalByQuery, long>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private IUserRolesDal _userRolesDal;
        public GetUserRolesListTotalHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, IUserRolesDal userRolesDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userRolesDal = userRolesDal;
        }
        public async Task<long> Handle(GetUserRolesListTotalByQuery request, CancellationToken cancellationToken)
        {
            var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
            string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.RoleModel);
            return await _userRolesDal.GetUserRoleListTotalCount(collectionName, request);
        }
    }
}
