using DatabaseLayer.Queries;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Model;
using PremiseGlobalLibrary;
using PremiseGlobalLibrary.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GroundFloor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroundFloorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GroundFloorController> _logger;
        private IPremiseService _premiseService;
        public GroundFloorController(IPremiseService premiseService, IMediator mediator, ILogger<GroundFloorController> logger)
        {
            _premiseService = premiseService;
            _mediator = mediator;
            _logger = logger;
        }
        [Authorize]
        [Route("buildings/{buildingid}/sensors/{type}")]
        public async Task<IActionResult> GetBuildingSensorsAsync(string buildingid, string type)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>();
            string cachKey = appKey + "_" + buildingid + "_" + type + "_sensorList";
            var cachData = await _premiseService.GetRecordFromCacheAsync<IEnumerable<Dictionary<string, object>>>(cachKey);

            if (cachData is null || cachData.Count() == 0)
            {
                var accessToken = Request.Headers[HeaderNames.Authorization];
                var result = await _mediator.Send(new GetBuildingSensors { BuildingID = buildingid, SensorType = type, Token = accessToken });
                await _premiseService.StroeRecordInCacheAsync<IEnumerable<Dictionary<string, object>>>(cachKey, result, TimeSpan.FromMinutes(5));
                returnData.data = result;
            }
            else
            {
                returnData.data = cachData;
            }
            returnData.status = "success";
            returnData.statusCode = StatusCodes.Status200OK;
            return StatusCode(StatusCodes.Status200OK, returnData);
        }

        [Authorize]
        [Route("buildings/{userid}")]
        public async Task<IActionResult> GetUserBuildingListAsync(string userid)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<IEnumerable<GroundFloorBuildings>>();
            string cachKey = appKey + "_" + userid + "_buildinglist";
            var cachData = await _premiseService.GetRecordFromCacheAsync<IEnumerable<GroundFloorBuildings>>(cachKey);
            if (cachData is null || cachData.Count() == 0)
            {
                var result = await _mediator.Send(new GetUserBuildingList { AppKey = appKey, UserID = userid });
                await _premiseService.StroeRecordInCacheAsync<IEnumerable<GroundFloorBuildings>>(cachKey, result, TimeSpan.FromMinutes(5));
                returnData.data = result;
            }
            else
            {
                returnData.data = cachData;
            }
            returnData.status = "success";
            returnData.statusCode = StatusCodes.Status200OK;
            return StatusCode(StatusCodes.Status200OK, returnData);
        }

        [Authorize]
        [Route("appsettings/{appid}/{settingkey}")]
        public async Task<IActionResult> GetAppSettings(string appid, string settingkey)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<PremiseAppSettingsModel>();
            string cachKey = appKey + "_" + appid + "_" + settingkey + "_appsettings";
            var cachData = await _premiseService.GetRecordFromCacheAsync<PremiseAppSettingsModel>(cachKey);

            if (cachData is null)
            {
                var result = await _mediator.Send(new GetAppSettings { AppKey = appKey, AppID = appid, SettingKey = settingkey });
                await _premiseService.StroeRecordInCacheAsync<PremiseAppSettingsModel>(cachKey, result, TimeSpan.FromMinutes(5));
                returnData.data = result;
            }
            else
            {
                returnData.data = cachData;
            }
            returnData.status = "success";
            returnData.statusCode = StatusCodes.Status200OK;
            return StatusCode(StatusCodes.Status200OK, returnData);
        }

        [Authorize]
        [HttpGet]
        [Route("departments")]
        public async Task<IActionResult> GetUserDepartmentListAsync([FromQuery] Pagination paging)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<IEnumerable<UserDepartmentModel>>();

            string cachKey = appKey + "_" + paging.Skip + "_" + paging.Limit + "_" + "userdepartmentlist";
            var cachData = await _premiseService.GetRecordFromCacheAsync<IEnumerable<UserDepartmentModel>>(cachKey);

            if (cachData is null || cachData.Count() == 0)
            {
                var result = await _mediator.Send(new GetUserDepartmentList { AppKey = appKey, Pagging = paging });
                await _premiseService.StroeRecordInCacheAsync<IEnumerable<UserDepartmentModel>>(cachKey, result, TimeSpan.FromMinutes(5));
                returnData.data = result;
            }
            else
            {
                returnData.data = cachData;
            }
            returnData.status = "success";
            returnData.statusCode = StatusCodes.Status200OK;
            return StatusCode(StatusCodes.Status200OK, returnData);
        }
    }
}
