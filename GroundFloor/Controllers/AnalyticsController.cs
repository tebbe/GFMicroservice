using DatabaseLayer.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Model;
using Model.QueryString;
using PremiseGlobalLibrary;
using PremiseGlobalLibrary.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GroundFloor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GroundFloorController> _logger;
        private IPremiseService _premiseService;
        public AnalyticsController(IPremiseService premiseService, IMediator mediator, ILogger<GroundFloorController> logger)
        {
            _premiseService = premiseService;
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        [Route("{buildingId}/{currentDate}/eodrunning")]

        public async Task<IActionResult> GetRunningEOD(string buildingid,string currentDate)    
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;

            var returnData = new ApiResponseSuccess<EODRunning>();
            string cachKey = appKey + "_" + buildingid + "_eodrunning";
            var cachData = await _premiseService.GetRecordFromCacheAsync<EODRunning>(cachKey);

            if (cachData is null)
            {
                var result = await _mediator.Send(new GetEODRunning {AppKey=appKey ,BuildingID = buildingid,CurrentMonthDate=currentDate});
                await _premiseService.StroeRecordInCacheAsync<EODRunning>(cachKey, result, TimeSpan.FromMinutes(5));
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
        [HttpPost]
        [Route("Geteodreport")]

        public async Task<IActionResult> GetEODReport([FromBody] EODReportQueryModel quaryParams)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;

            var returnData = new ApiResponseSuccess<IEnumerable<EODReport>>();
            string cachKey = appKey + "_" + quaryParams.BuildingId + "_"+ quaryParams.Type+ "_eodreport";
            var cachData = await _premiseService.GetRecordFromCacheAsync<IEnumerable<EODReport>>(cachKey);

            if (cachData is null)
            {
                var result = await _mediator.Send(new GetEODReport { AppKey = appKey, QueryModel = quaryParams});
                await _premiseService.StroeRecordInCacheAsync<IEnumerable<EODReport>>(cachKey, result, TimeSpan.FromMinutes(5));
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
