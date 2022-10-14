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
    public class GetRoleByDidHandler : IRequestHandler<GetRoleByDid, Dictionary<string, object>>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private IUserRolesDal _userRolesDal;
        public GetRoleByDidHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, IUserRolesDal userRolesDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userRolesDal = userRolesDal;
        }

        public async Task<Dictionary<string, object>> Handle(GetRoleByDid request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.RoleModel);
                return await _userRolesDal.GetRoleByDidAsync(collectionName, request.Did);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
