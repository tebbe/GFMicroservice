using DatabaseLayer.Dal;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.Extensions.Options;
using Model;
using Model.QueryString;
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
    public class GetUserByIdHandler:IRequestHandler<GetUserByDidQuery, Dictionary<string, object>>
    {
    private IPremiseService _premiseService;
    private IOptions<DBCollections> _dbCollections;
    private IUserDal _userDal;
    public GetUserByIdHandler(IUserDal userDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
    {
        _premiseService = premiseService;
        _dbCollections = dbCollections;
        _userDal = userDal;
    }

        public async Task<Dictionary<string, object>> Handle(GetUserByDidQuery request, CancellationToken cancellationToken)
        {
            var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
            var collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.UserSystem);
            return await _userDal.GetByIdAsync(collectionName, request.UserId);
        }
    }
}
