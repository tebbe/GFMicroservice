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
    public class EquipmentsControllerTests
    {
        public Helper helper = new Helper();
        [Fact]
        public async Task GetPMDemoScheduleListAsync()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>>(token, "https://localhost:44347/api/equipments");
            Assert.Equal(200, data.statusCode);
        }
    }
}
