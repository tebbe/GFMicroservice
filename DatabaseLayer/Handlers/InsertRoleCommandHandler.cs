using DatabaseLayer.Commands;
using DatabaseLayer.Dal;
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
    public class InsertRoleCommandHandler : IRequestHandler<InsertRoleCommand, string>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private IUserRolesDal _userRolesDal;
        public InsertRoleCommandHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, IUserRolesDal userRolesDal )
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userRolesDal = userRolesDal;
        }

        public async Task<string> Handle(InsertRoleCommand request, CancellationToken cancellationToken)
        {
            var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
            string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.RoleModel);

            return await _userRolesDal.SaveAsync(collectionName, request.RoleModel);
        }
    }
}
