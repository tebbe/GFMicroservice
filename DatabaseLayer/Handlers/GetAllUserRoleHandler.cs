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
    public class GetAllUserRoleHandler : IRequestHandler<GetAllUserRoles, IEnumerable<Dictionary<string, object>>>
    {
        private IUserRolesDal _UserRolesDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetAllUserRoleHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, IUserRolesDal UserRolesDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _UserRolesDal = UserRolesDal;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> Handle(GetAllUserRoles request, CancellationToken cancellationToken)
        {
            var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
            string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.RoleModel);

            return  await _UserRolesDal.GetRoleAsync(collectionName,request);
            
        }
    }
}
