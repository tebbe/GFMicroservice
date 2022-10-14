using Model;
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
    public class UserPermissionsControllerTests
    {
        public Helper helper = new Helper();
        string Did = "";

        [Fact]
        public async Task PostAsync()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            UserPermissionModel queryParams = new UserPermissionModel()
            {
                AppId = "DemoUpdateTest110822",
                Description = "DemoUpdateTest110822",
                PermissionKey = "DemoUpdateTest110822",
                PermissionName = "DemoUpdateTest110822"
            };
            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/UserPermissions", JsonConvert.SerializeObject(queryParams));
            Did = data.data["Did"].ToString();
            Assert.Equal(201, data.statusCode);

        }

        [Fact]
        public async Task PutAlertSettings()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            UserPermissionModel queryParams = new UserPermissionModel()
            {
                AppId = "DemoUpdateTest110822",
                Description = "DemoUpdateTest110822",
                PermissionKey = "DemoUpdateTest110822",
                PermissionName = "DemoUpdateTest110822"
            };
            var data = await helper.PutDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/UserPermissions", JsonConvert.SerializeObject(queryParams));
            Assert.Equal(200, data.statusCode);
            Assert.True(data.data.Count != 0);
        }

        [Fact]
        public async Task GetUserPermissionByDid()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/UserPermissions/20ad46c3cc6b4328ba4e672a3f29e3d0");
            Assert.Equal(200, data.statusCode);
        }

        [Fact]
        public async Task GetUserPermissionByQuery()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.GetDataFromApiAsync<ApiResponseSuccess<List<Dictionary<string, object>>>>(token, "https://localhost:44347/api/UserPermissions?AppId=Tenant");
            Assert.Equal(200, data.statusCode);
        }
    }
}
