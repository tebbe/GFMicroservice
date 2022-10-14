using DatabaseLayer.Commands;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PremiseGlobalLibrary;
using PremiseGlobalLibrary.Model;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Linq;
using Model.QueryString;
using Model;
namespace GroundFloor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IPremiseService _premiseService;

        public UserPermissionsController(IPremiseService premiseService, IMediator mediator)
        {
            _mediator = mediator;
            _premiseService = premiseService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserPermissionModel model)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            string userID = identity.FindFirst("jid").Value;
            var returnData = new ApiResponseSuccess<Dictionary<string, object>>();

            bool exist = await _mediator.Send(new CheckUserPermissionByName { Appkey = appKey, PermissionName = model.PermissionName, PermissionKey = model.PermissionKey, Did = "" });
            if (!exist)
            {
                var did = await _mediator.Send(new InsertUserPermissionCommand(userID, model) { AppKey = appKey });

                returnData.statusCode = StatusCodes.Status201Created;
                returnData.status = "success";
                returnData.message = "Information has been saved successfully";
                returnData.data = new Dictionary<string, object> { { "Did", did } };
                return StatusCode(StatusCodes.Status201Created, returnData);
            }
            else
            {
                returnData.statusCode = StatusCodes.Status200OK;
                returnData.status = "success";
                returnData.message = "Same user permission exist";
                return StatusCode(StatusCodes.Status200OK, returnData);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] UserPermissionModel model)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            string userID = identity.FindFirst("jid").Value;
            var returnData = new ApiResponseSuccess<Dictionary<string, object>>();

            bool exist = await _mediator.Send(new CheckUserPermissionByName { Appkey = appKey, PermissionName = model.PermissionName, PermissionKey = model.PermissionKey, Did = model.Did });
            if (!exist)
            {
                var did = await _mediator.Send(new UpdateUserPermissionCommand(userID, model) { AppKey = appKey });
                returnData.statusCode = StatusCodes.Status200OK;
                returnData.status = "success";
                returnData.message = "Information has been updated successfully";
                returnData.data = new Dictionary<string, object> { { "Did", did } };
                return StatusCode(StatusCodes.Status200OK, returnData);
            }
            else
            {
                returnData.statusCode = StatusCodes.Status200OK;
                returnData.status = "success";
                returnData.message = "Same user permission exist";
                return StatusCode(StatusCodes.Status200OK, returnData);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("{did}")]
        public async Task<IActionResult> GetUserPermissionByDidAsync(string did)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<Dictionary<string, object>>();

            var result = await _mediator.Send(new GetUserPermissionByDid { AppKey = appKey, Did = did });

            returnData.statusCode = StatusCodes.Status200OK;
            returnData.status = "success";
            returnData.message = "";
            returnData.data = result;
            return StatusCode(StatusCodes.Status200OK, returnData);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserPermission([FromQuery] UserPermissionQueryModel queryModel, [FromQuery] Pagination paging)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>();
            var result = await _mediator.Send(new GetUserPermissionByQuery { AppKey = appKey, QueryParam = queryModel, Paging = paging });
            var counttask = _mediator.Send(new GetUserPermissionTotalByQuery { AppKey = appKey, QueryParam = queryModel });

            HttpContext.Response.Headers.Add("X-Total-Count", (await counttask).ToString());
            HttpContext.Response.Headers.Add("X-Showing", result.ToList().Count().ToString());
            HttpContext.Response.Headers.Add("X-Offset", paging.Skip.ToString());
            returnData.data = result;
            returnData.status = "success";
            returnData.statusCode = StatusCodes.Status200OK;
            return StatusCode(StatusCodes.Status200OK, returnData);
        }
    }
}
