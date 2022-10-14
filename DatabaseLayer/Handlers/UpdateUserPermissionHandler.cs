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
    public class UpdateUserPermissionHandler : IRequestHandler<UpdateUserPermissionCommand, string>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private UserPermissionDal _userPermissionDal;
        public UpdateUserPermissionHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, UserPermissionDal userPermissionDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userPermissionDal = userPermissionDal;
        }

        public async Task<string> Handle(UpdateUserPermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.UserPermission);

                return await _userPermissionDal.UpdateAsync(collectionName, request.userPermissionModel);
            }
            catch
            {
                throw;
            }
        }
    }
}
