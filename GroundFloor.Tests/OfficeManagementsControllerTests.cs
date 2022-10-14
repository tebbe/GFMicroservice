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
    public class OfficeManagementsControllerTests
    {
        public Helper helper = new Helper();
        [Fact]
        public async Task GetResourcesFlatListAsync()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<IEnumerable<GFResourcesFlatModel>>>(token, "https://localhost:44347/api/OfficeManagements/resourcesflat/eecd0835a0b14dd3b569e2339dcc9183");
            Assert.Equal(200, data.statusCode);
        }

        [Fact]
        public async Task GetResourcesListAsync()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>>(token, "https://localhost:44347/api/OfficeManagements/resources");
            Assert.Equal(200, data.statusCode);
        }

        [Fact]
        public async Task GetTeamsListAsync()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>>(token, "https://localhost:44347/api/OfficeManagements/teams");
            Assert.Equal(200, data.statusCode);
        }
    }
}
