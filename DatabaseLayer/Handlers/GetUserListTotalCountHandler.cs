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
    public class GetUserListTotalCountHandler : IRequestHandler<GetUserListTotalByQuery, long>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private IUserDal _userDal;
        public GetUserListTotalCountHandler(IUserDal userDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userDal = userDal;
        }

        public async Task<long> Handle(GetUserListTotalByQuery request, CancellationToken cancellationToken)
        {
            var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
            string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.UserSystem);
            return await _userDal.GetUserListTotalCount(collectionName, request);
        }
    }
}
