using GroundFloor.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PremiseGlobalLibrary;
using PremiseGlobalLibrary.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace GroundFloor.Tests
{
    public class AlertsControllerTests
    {
        //public Helper helper = new Helper();

        //[Fact]
        //public async Task GetAlertByQueryAsync()
        //{
        //    string token = "Bearer " + await helper.GetTokenAsync();
        //    var data = await helper.GetDataFromApiAsync<ApiResponseSuccess<List<AlertModel>>>(token, "https://localhost:44347/api/alerts?Buildingid=d6928042dc71439ba2f24f60fbbfe010&Floorid=fb891cb72dd9477d90169481eef18d48&Resolved=True");
        //    Assert.Equal(200, data.statusCode);
        //}
    }
}
