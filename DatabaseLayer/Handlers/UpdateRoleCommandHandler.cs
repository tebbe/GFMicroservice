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
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, string>
    {
        
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private IUserRolesDal _userRolesDal;
        public UpdateRoleCommandHandler(IPremiseService PremiseService, IOptions<DBCollections> DbCollections,IUserRolesDal UserRolesDal)
        {
            _premiseService = PremiseService;
            _dbCollections = DbCollections;
            _userRolesDal = UserRolesDal;
        }
        public async Task<string> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.RoleModel);

                return await _userRolesDal.UpdateAsync(collectionName , request.RoleModel);
            }

            catch
            {
                throw;
            }
        }
    }
}
