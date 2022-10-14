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
    public class OfficeManagementsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OfficeManagementsController> _logger;
        private IPremiseService _premiseService;
        public OfficeManagementsController(IPremiseService premiseService, IMediator mediator, ILogger<OfficeManagementsController> logger)
        {
            _premiseService = premiseService;
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        [Route("resourcesflat")]
        public async Task<IActionResult> GetResourcesFlatListAsync([FromQuery] string floorid)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<IEnumerable<GFResourcesFlatModel>>();

            string cachKey = appKey + "_" + floorid + "resourcesflatlist";
            var cachData = await _premiseService.GetRecordFromCacheAsync<IEnumerable<GFResourcesFlatModel>>(cachKey);

            if (cachData is null || cachData.Count() == 0)
            {
                var result = await _mediator.Send(new GetResourcesFlatList { AppKey = appKey, FloorId = floorid });
                await _premiseService.StroeRecordInCacheAsync<IEnumerable<GFResourcesFlatModel>>(cachKey, result, TimeSpan.FromSeconds(5));
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
        [Route("resources")]
        public async Task<IActionResult> GetResourcesListAsync()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>();

            string cachKey = appKey + "_" + "resourceslist";
            var cachData = await _premiseService.GetRecordFromCacheAsync<IEnumerable<Dictionary<string, object>>>(cachKey);

            if (cachData is null || cachData.Count() == 0)
            {
                var result = await _mediator.Send(new GetResourcesList { AppKey = appKey });
                await _premiseService.StroeRecordInCacheAsync<IEnumerable<Dictionary<string, object>>>(cachKey, result, TimeSpan.FromSeconds(5));
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
        [Route("teams")]
        public async Task<IActionResult> GetTeamsListAsync()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>();

            string cachKey = appKey + "_" + "teamslist";
            var cachData = await _premiseService.GetRecordFromCacheAsync<IEnumerable<Dictionary<string, object>>>(cachKey);

            if (cachData is null || cachData.Count() == 0)
            {
                var result = await _mediator.Send(new GetTeamsList { AppKey = appKey });
                await _premiseService.StroeRecordInCacheAsync<IEnumerable<Dictionary<string, object>>>(cachKey, result, TimeSpan.FromSeconds(5));
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
