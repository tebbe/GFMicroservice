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
using Model.UserSystem;
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
    public class ProvincesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProvincesController> _logger;
        private IPremiseService _premiseService;
        public ProvincesController(IPremiseService premiseService, IMediator mediator, ILogger<ProvincesController> logger)
        {
            _premiseService = premiseService;
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [Route("{province}")]
        [HttpPost]
        public async Task<IActionResult> GetProvinceListAsync(string province, [FromQuery] Pagination paging)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;

            var returnData = new ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>();

            
            var result = await _mediator.Send(new GetProvince { AppKey = appKey, Province = province, Paiging = paging });
            
            returnData.data = result;
            returnData.status = "success";
            returnData.statusCode = StatusCodes.Status200OK;
            return Ok(returnData);
        }

        [Authorize]
        [HttpGet]
        [Route("{did}")]
        public async Task<IActionResult> GetProvinceAsync(string did)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<Dictionary<string, object>>();

            var result = await _mediator.Send(new GetProvinceByUserID { AppKey = appKey, ProvinceID = did });

            returnData.statusCode = StatusCodes.Status200OK;
            returnData.status = "success";
            returnData.message = "";
            returnData.data = result;
            return StatusCode(StatusCodes.Status200OK, returnData);
        }
    }
}
