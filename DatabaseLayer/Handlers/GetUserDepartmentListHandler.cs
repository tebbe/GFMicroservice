using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DatabaseLayer.Dal;
using DatabaseLayer.DBContext;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.Extensions.Options;
using Model;
using PremiseGlobalLibrary;

namespace DatabaseLayer.Handlers
{
    class GetUserDepartmentListHandler : IRequestHandler<GetUserDepartmentList, IEnumerable<UserDepartmentModel>>
    {
        private UserDepartmentDal _userDepartmentDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
       
        public GetUserDepartmentListHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, UserDepartmentDal userDepartmentDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userDepartmentDal = userDepartmentDal;
        }
        public async Task<IEnumerable<UserDepartmentModel>> Handle(GetUserDepartmentList request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string userDepartmentCollectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.UserDepartment);
                return await _userDepartmentDal.GetAsync(userDepartmentCollectionName, request.Pagging.Skip, request.Pagging.Limit);
            }
            catch
            {
                throw;
            }
        }
    }
}
