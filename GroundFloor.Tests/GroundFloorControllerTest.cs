using Model;
using Model.QueryString;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PremiseGlobalLibrary.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace GroundFloor.Tests
{
    public class GroundFloorControllerTest
    {

        //public Helper helper = new Helper();
        //[Fact]
        //public async Task GetUserBuildingListAsync()
        //{          
        //    string token = "Bearer " + await helper.GetTokenAsync();
        //    var data = await helper.GetDataFromApiAsync<ApiResponseSuccess<List<GroundFloorBuildings>>>(token, "https://localhost:44347/api/groundfloor/buildings/a43ca2660d6942b891982aa312bdcf49");
        //    Assert.Equal(200,data.statusCode);
        //}
        //[Fact]
        //public async Task GetUserDepartmentListAsync()
        //{
        //    string token = "Bearer " + await helper.GetTokenAsync();
        //    var data = await helper.GetDataFromApiAsync<ApiResponseSuccess<List<UserDepartmentModel>>>(token, "https://localhost:44347/api/groundfloor/departments?skip=0&limit=2");
        //    Assert.Equal(200, data.statusCode);
        //    Assert.Equal(2, data.data.Count);
        //}

        //[Fact]
        //public async Task GetBuildingSensorsAsync()
        //{
        //    string token = "Bearer " + await helper.GetTokenAsync();
        //    string buildingID = "64a6a810e7124548bfae7954d5f8c231";
        //    string sensorType = "inout";
        //    var data = await helper.GetDataFromApiAsync<ApiResponseSuccess<List<Dictionary<string, object>>>>(token, "https://localhost:44347/api/groundfloor/buildings/" + buildingID + "/sensors/" + sensorType);
        //    Assert.Equal(200, data.statusCode);
        //    Assert.True(data.data.Count > 0);
        //}

        //[Fact]
        //public async Task GetAppSettings()
        //{
        //    string token = "Bearer " + await helper.GetTokenAsync();
        //    string appID = "GroundFloor";
        //    string settingKey = "GroundFloorLeftMenu";
        //    var data = await helper.GetDataFromApiAsync<ApiResponseSuccess<PremiseAppSettingsModel>>(token, "https://localhost:44347/api/groundfloor/appsettings/" + appID +"/" + settingKey);
        //    Assert.Equal(200, data.statusCode);
        //    Assert.NotNull(data.data);
        //}

       
    }
}
