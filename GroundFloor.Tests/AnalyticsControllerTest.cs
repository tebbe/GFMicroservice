using Model.QueryString;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PremiseGlobalLibrary.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GroundFloor.Tests
{
    public class AnalyticsControllerTest
    {
        //public Helper helper = new Helper();

        //[Fact]
        //public async Task GetRunningEODTest()
        //{
        //    string token = "Bearer " + await helper.GetTokenAsync();
        //    string buildingId = "8b5a0394729947b09883475548aaf7a4";
        //    string currentDate = "2022-02-01";
        
        //    var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/Analytics/" + buildingId + "/"+currentDate + "/eodrunning");
        //    Assert.Equal(200, data.statusCode);
        //    Assert.NotNull(data.data);
        //    Assert.True(data.data.Count > 0);
        //}


        //[Fact]
        //public async Task GetEODReportTest()
        //{
           
        //    EODReportQueryModel queryParams = new EODReportQueryModel()
        //    {
        //        BuildingId="8b5a0394729947b09883475548aaf7a4",
        //        Type="Day",
        //        Others = new string[] {  "2022-01-24","2022-01-25","2022-01-26","2022-01-27","2022-01-28","2022-01-29","2022-01-30" }
        //    };
        //    var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/Analytics/Geteodreport", JsonConvert.SerializeObject(queryParams));
        //    Assert.Equal(200, data.statusCode);
        //    Assert.NotNull(data.data);
        //    Assert.True(data.data.Count > 0);
        //}



    }
}
