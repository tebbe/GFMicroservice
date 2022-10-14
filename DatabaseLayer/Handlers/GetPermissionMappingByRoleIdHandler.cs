using DatabaseLayer.Dal;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.Extensions.Options;
using Model;
using Model.QueryString;
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
    public class GetPermissionMappingByRoleIdHandler : IRequestHandler<GetPermissionByRoleId, Dictionary<string,object>>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private IUserDal _userDal;
        public GetPermissionMappingByRoleIdHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, IUserDal userDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userDal = userDal;
            
        }

        public async Task<Dictionary<string, object>> Handle(GetPermissionByRoleId request, CancellationToken cancellationToken)
        {
            var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
            
            string rolePreMappingCollection = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.RolePermissionMapping);
            var rolePermissionMapping = await _userDal.GetRolePermissionMapping(rolePreMappingCollection, request.RoleId);

            return rolePermissionMapping;
        }
    }
}
