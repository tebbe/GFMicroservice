using DatabaseLayer.Commands;
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
    public class PermissionMappingUpdateHandler : IRequestHandler<UpdateRolePermissionMappingCommand, bool>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private IUserDal _userDal;
        public PermissionMappingUpdateHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, IUserDal userDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userDal = userDal;
            
        }

        public async Task<bool> Handle(UpdateRolePermissionMappingCommand model, CancellationToken cancellationToken)
        {

            var appInfo = await _premiseService.GetAppNameAsync(model.AppKey);
            string rolePreMappingCollection = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.RolePermissionMapping);
            var rolePermissionMapping = await _userDal.UpdateRolePermissionMapping(rolePreMappingCollection, model.permissionModel);

            return rolePermissionMapping;
        }
    }
}
