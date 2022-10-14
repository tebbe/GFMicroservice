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
    public class ResponsibilityReAssignIntegrationHandler:IRequestHandler<ResponsibilityIntegrationQuery,IEnumerable<Dictionary<string,object>>>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private IUserDal _userDal;
        public ResponsibilityReAssignIntegrationHandler(IPremiseService PremiseService, IOptions<DBCollections> DbCollections, IUserDal userDal)
        {
            _premiseService = PremiseService;
            _dbCollections = DbCollections;
            _userDal = userDal; 
          
        }

        public async Task<IEnumerable<Dictionary<string, object>>> Handle(ResponsibilityIntegrationQuery request, CancellationToken cancellationToken)
        {
            var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
            string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.APIIntegration);

            return await _userDal.GetAPIIntegration(collectionName);
        }
    }
}
