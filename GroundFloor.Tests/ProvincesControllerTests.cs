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
    public class ProvincesControllerTests
    {
        public Helper helper = new Helper();
        [Fact]
        public async Task GetProvinceListAsync()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>>(token, "https://localhost:44347/api/Provinces/Alberta?Limit=2");
            Assert.Equal(200, data.statusCode);
        }

        [Fact]
        public async Task GetProvinceByDidAsync()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.GetDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/Provinces/2ad34df65e264528b4d38fa516b40054");
            Assert.Equal(200, data.statusCode);
        }
    }
}
