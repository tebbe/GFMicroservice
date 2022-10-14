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
using Model.QueryString;
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
    public class AlertsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AlertsController> _logger;
        private IPremiseService _premiseService;
        public AlertsController(IPremiseService premiseService, IMediator mediator, ILogger<AlertsController> logger)
        {
            _premiseService = premiseService;
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AlertQueryModel queryparam, [FromQuery] Pagination paging)
        {


            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<IEnumerable<AlertModel>>();
            var result = await _mediator.Send(new GetAlertByQuery { AppKey = appKey,QueryParam = queryparam,Paging = paging });
            var counttask = _mediator.Send(new GetAlertTotalCount { AppKey = appKey, QueryParam = queryparam });
            returnData.data = result;

            Int64 count = await counttask;
            returnData.status = "success";
            returnData.statusCode = StatusCodes.Status200OK;
            HttpContext.Response.Headers.Add("X-Total-Count", count.ToString());
            return StatusCode(StatusCodes.Status200OK, returnData);
        }

    }
}
