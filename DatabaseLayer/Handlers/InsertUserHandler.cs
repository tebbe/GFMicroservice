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
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, string>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private IUserDal _userDal;  
        public InsertUserHandler(IUserDal userDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userDal = userDal;
        }

        public async Task<string> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
            string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.UserSystem);

            return await _userDal.SaveAsync(collectionName, request.UserModel);
        }
    }
}
