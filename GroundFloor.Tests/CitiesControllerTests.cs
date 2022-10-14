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
    public class CitiesControllerTests
    {
        public Helper helper = new Helper();
        [Fact]
        public async Task GetCityByIDAsync()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.GetDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/Cities/c8c34c78ed8d4de0bfb7772ce9192a9a");
            Assert.Equal(200, data.statusCode);
        }

        [Fact]
        public async Task GetCityByProvinceIDOrCityAsync()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>>(token, "https://localhost:44347/api/cities/2ad34df65e264528b4d38fa516b40054/Banff");
            Assert.Equal(200, data.statusCode);
        }
    }
}
