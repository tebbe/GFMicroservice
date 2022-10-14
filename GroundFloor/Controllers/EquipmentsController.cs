using DatabaseLayer.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PremiseGlobalLibrary;
using PremiseGlobalLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GroundFloor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IPremiseService _premiseService;
        public EquipmentsController(IPremiseService premiseService, IMediator mediator)
        {
            _mediator = mediator;
            _premiseService = premiseService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetPMDemoScheduleList()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;

            var returnData = new ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>();
            string cachKey = appKey + "_" + "pmdemoschedule";
            var cachData = await _premiseService.GetRecordFromCacheAsync<IEnumerable<Dictionary<string, object>>>(cachKey);

            if (cachData is null || cachData.Count() == 0)
            {
                var result = await _mediator.Send(new GetPMDemoScheduleList { AppKey = appKey });
                await _premiseService.StroeRecordInCacheAsync<IEnumerable<Dictionary<string, object>>>(cachKey, result, TimeSpan.FromSeconds(5));
                returnData.data = result;
            }
            else
            {
                returnData.data = cachData;
            }
            returnData.status = "success";
            returnData.statusCode = StatusCodes.Status200OK;
            return Ok(returnData);
        }

        
    }
}
