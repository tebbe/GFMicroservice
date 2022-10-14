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
    public class ResponsibilityReAssignHandler : IRequestHandler<ResponsibilityReAssignQuery, IEnumerable<Dictionary<string, object>>>
    {

        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private IUserDal _userDal;
        public ResponsibilityReAssignHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, IUserDal userDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userDal = userDal;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> Handle(ResponsibilityReAssignQuery request, CancellationToken cancellationToken)
        {
            var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
            string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.UserSystem);

            return await _userDal.GetResponsibilityReAssign(collectionName, request);
        }
    }
}
