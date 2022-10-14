using DatabaseLayer.Dal;
using DatabaseLayer.DBContext;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseLayer.Handlers
{
    class GetBuildingSensorsHandler : IRequestHandler<GetBuildingSensors, IEnumerable<Dictionary<string, object>>>
    {
        private HttpClient _client = new HttpClient();
        private readonly IConfiguration _configuration;

        public GetBuildingSensorsHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<Dictionary<string, object>>> Handle(GetBuildingSensors request, CancellationToken cancellationToken)
        {
            try
            {
               
                var zoneApiUrl = request.SensorType.ToLower() != "all" ? String.Format(_configuration.GetSection("ApiList:GetSensorsByBuildingAndTypeFromZone").Value, request.BuildingID,request.SensorType) :
                    String.Format(_configuration.GetSection("ApiList:GetAllSensorsByBuilsingFromZone").Value, request.BuildingID);

                Task<List<ZoneModel>> zList =GetDataFromApiAsync<List<ZoneModel>>(request.Token, zoneApiUrl);
                Task<List<FloorModel>> fList =  GetDataFromApiAsync<List<FloorModel>>(request.Token, String.Format(_configuration.GetSection("ApiList:GetFloorsByBuilding").Value,request.BuildingID));
                Task<List<SuiteModel>> sList =  GetDataFromApiAsync<List<SuiteModel>>(request.Token, String.Format(_configuration.GetSection("ApiList:GetSuitesByBuilding").Value, request.BuildingID));

                var zoneList = await zList;
                var floorList = await fList;
                var suiteList = await sList;

                List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
                Parallel.ForEach(zoneList,zoneData =>
                 {
                     string serial = zoneData.SerialNumber;
                     string floorID = zoneData.FloorID.ToString();
                     string floorNo = floorList.Where(m => m.Did == floorID).Select(m => m.Floor).FirstOrDefault();

                     if (!string.IsNullOrEmpty(serial))
                     {
                         string tempSuiteNo = "";
                         string tempSuiteID = "";
                         try
                         {
                             string AggregateZonesID = zoneData.AggregateZones;
                             string SuiteId = zoneList.Where(m => m.Did == AggregateZonesID).Select(m => m.SuiteId).FirstOrDefault();
                             tempSuiteID = string.IsNullOrEmpty(SuiteId) ? "" : SuiteId;
                             string suiteNo = suiteList.Where(m => m.Did == SuiteId).Select(m => m.SuiteNo).FirstOrDefault();
                             tempSuiteNo = string.IsNullOrEmpty(suiteNo) ? "" : suiteNo;
                         }
                         catch
                         {
                             tempSuiteID = "";
                             tempSuiteNo = "";
                         }

                         result.Add(new Dictionary<string, object>()
                         {
                            {"gateway",zoneData.Gateway},
                            {"threshold",zoneData.Threshold},
                            {"buildingID",request.BuildingID},
                            {"displayName",zoneData.DisplayName},
                            {"serial",serial},
                            {"zoneID",serial},
                            {"zoneName",zoneData.Name},
                            {"zoneType",zoneData.ZoneType},
                            {"floorNumber",floorNo},
                            {"floorID",floorID},
                            {"spaceType",zoneData.SpaceType},
                            {"suiteNo",tempSuiteNo},
                            {"suiteID",tempSuiteID},
                            {"deviceType",zoneData.SensorType},
                            {"deviceID",tempSuiteID}
                         });
                     }
                 });
                return result;
            }
            catch
            {
                throw;
            }
        }

        private async Task<T> GetDataFromApiAsync<T>(string token,string apiUrl)
        {

            using (var requestMessage =new HttpRequestMessage(HttpMethod.Get, apiUrl))
            {
                string[] splitToken = token.Split(" ");
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(splitToken[0].Trim(), splitToken[1].Trim());

                var  result = await _client.SendAsync(requestMessage);
                var jsonString = await result.Content.ReadAsStringAsync();
                JObject m = JObject.Parse(jsonString);
                return JsonConvert.DeserializeObject<T>(m["data"].ToString());
            }
        }
    }
}
