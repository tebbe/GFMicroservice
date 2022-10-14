using DatabaseLayer.Commands;
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
    public class RoleUserMappingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RoleUserMappingsController> _logger;
        private IPremiseService _premiseService;
        public RoleUserMappingsController(IPremiseService premiseService, IMediator mediator, ILogger<RoleUserMappingsController> logger)
        {
            _premiseService = premiseService;
            _mediator = mediator;
            _logger = logger;
        }

        //[Authorize]
        //[HttpPost]
        //[Route("{userid}")]
        //public async Task<ActionResult> DeleteAsync(string userid)
        //{
        //    var identity = HttpContext.User.Identity as ClaimsIdentity;
        //    string appKey = identity.FindFirst("jak").Value;
        //    string userID = identity.FindFirst("jid").Value;
        //    var returnData = new ApiResponseSuccess<Dictionary<string, object>>();
        //    var result = await _mediator.Send(new DeleteRoleUserMappingCommand { AppKey = appKey, UserID = userid });
        //    returnData.statusCode = StatusCodes.Status200OK;
        //    returnData.status = "success";
        //    returnData.message = "Information has been deleted successfully";
        //    return StatusCode(StatusCodes.Status200OK, returnData);
        //}
    }
}
