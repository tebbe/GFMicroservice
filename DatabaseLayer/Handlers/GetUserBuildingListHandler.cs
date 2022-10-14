using DatabaseLayer.Dal;
using DatabaseLayer.DBContext;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.Extensions.Options;
using Model;
using Newtonsoft.Json;
using PremiseGlobalLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseLayer.Handlers
{
    class GetUserBuildingListHandler : IRequestHandler<GetUserBuildingList, IEnumerable<GroundFloorBuildings>>
    {
        private IDBContext _dbContext;
        private AppSettingsDal _appSettingsDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private UserSystemDal _userSystemDal;

        public GetUserBuildingListHandler(IDBContext dbContext, UserSystemDal userSystemDal, AppSettingsDal appSettingsDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _dbContext = dbContext;
            _appSettingsDal = appSettingsDal;
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userSystemDal = userSystemDal;

        }
        public async Task<IEnumerable<GroundFloorBuildings>> Handle(GetUserBuildingList request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string userCollectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.UserSystem);
                string apppsettingCollection = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.PremiseAppSettings);

                Task<UserSystemModel>uInfo =  _userSystemDal.GetAsync(userCollectionName, request.UserID);
                Task<PremiseAppSettingsModel> gbInfo =  _appSettingsDal.GetAsync(apppsettingCollection, "OperationalStack", "BuildingSettings");

                var userInfo = await uInfo;
                var groundFloorBuilding = await gbInfo;
                if (userInfo !=null)
                {
                    string Property = userInfo.PropertyBuilding;
                    List<UserBuildingList> buildingList = JsonConvert.DeserializeObject<List<UserBuildingList>>(Property);

                    if (groundFloorBuilding!=null)
                    {
                        List<GroundFloorBuildings> finalResult = new List<GroundFloorBuildings>();
                        List<GroundFloorBuildings> permittedBuilding = JsonConvert.DeserializeObject<List<GroundFloorBuildings>>(groundFloorBuilding.SettingValue.ToString());

                       Parallel.ForEach(buildingList, userSingleBuilding=>
                        {
                            var temp = permittedBuilding.Where(m => m.BuildingId == userSingleBuilding.BuildingId).FirstOrDefault();
                            if (temp != null)
                                finalResult.Add(temp);
                        });
                        return finalResult;
                    }
                    else
                    {
                        throw new Exception("No setting found");
                    }
                }
                else
                {
                    throw new Exception("User not found");
                }
            }
            catch
            {
                throw;
            }
            
        }
    }
}
