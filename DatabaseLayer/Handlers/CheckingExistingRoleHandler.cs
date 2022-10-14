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
    public class CheckingExistingRoleHandler : IRequestHandler<CheckRoleByQuery, bool>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private IUserRolesDal _rolesDal;
        public CheckingExistingRoleHandler(IUserRolesDal rolesDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _rolesDal = rolesDal;
        }
        public async Task<bool> Handle(CheckRoleByQuery request, CancellationToken cancellationToken)
        {
            var appInfo = await _premiseService.GetAppNameAsync(request.Appkey);
            string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.RoleModel);

            return await _rolesDal.IsExist(collectionName, request.RoleName, request.Did);
        }
    }
}
