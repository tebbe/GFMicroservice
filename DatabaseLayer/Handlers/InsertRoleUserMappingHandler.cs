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
    public class InsertRoleUserMappingHandler : IRequestHandler<InsertRoleUserMappingCommand, bool>
    {
        private RoleUserMappingDal _roleusermappingDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        public InsertRoleUserMappingHandler(RoleUserMappingDal roleusermappingDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _roleusermappingDal = roleusermappingDal;
            _premiseService = premiseService;
            _dbCollections = dbCollections;
        }

        public async Task<bool> Handle(InsertRoleUserMappingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.RoleUserMapping);
                return await _roleusermappingDal.SaveAsync(collectionName, request.RoleUserMappingObj);

            }
            catch
            {
                throw;
            }
        }
    }
}
