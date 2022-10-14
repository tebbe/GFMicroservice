using DatabaseLayer.Commands;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.QueryString;
using Model.UserSystem;
using PremiseGlobalLibrary;
using PremiseGlobalLibrary.Model;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GroundFloor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSystemRolesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IPremiseService _premiseService;

        public UserSystemRolesController(IPremiseService premiseService, IMediator mediator)
        {
            _mediator = mediator;
            _premiseService = premiseService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetRoles([FromQuery] RolelistQueryModel queryModel,[FromQuery] Pagination pagination)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<IEnumerable<Dictionary<string,object>>>();
            var result = await _mediator.Send(new GetAllUserRoles { AppKey = appKey,QueryModel = queryModel,Paging = pagination});
            var count = await _mediator.Send(new GetUserRolesListTotalByQuery { AppKey = appKey, QueryParam = queryModel, Paging = pagination });
            HttpContext.Response.Headers.Add("X-Total-Count", count.ToString());
            returnData.data = result;
            returnData.status = "success";
            returnData.statusCode = StatusCodes.Status200OK;
            return StatusCode(StatusCodes.Status200OK, returnData);
        }
       
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] RoleModel model)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            string userID = identity.FindFirst("jid").Value;
            var returnData = new ApiResponseSuccess<Dictionary<string, object>>();
            if (!IsUserExist(model.RoleName,"").Result)
            {
                var role = await _mediator.Send(new InsertRoleCommand(userID, model) { AppKey = appKey });
                
                returnData.statusCode = StatusCodes.Status201Created;
                returnData.status = "success";
                returnData.message = "Information has been saved successfully";
                returnData.data = new Dictionary<string, object> { { "Did", role } };
                return StatusCode(StatusCodes.Status201Created, returnData);
            }
            else
            {
                returnData.statusCode = StatusCodes.Status200OK;
                returnData.status = "success";
                returnData.message = "Same role exist";
                returnData.data = new Dictionary<string, object> { { "Did", "" } };
                return StatusCode(StatusCodes.Status200OK, returnData);
            }
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateRole([FromBody] RoleModel model)

        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            string userID = identity.FindFirst("jid").Value;
            var returnData = new ApiResponseSuccess<Dictionary<string, object>>();
            if (!IsUserExist(model.RoleName, model.Did).Result)
            {
                var role = await _mediator.Send(new UpdateRoleCommand(userID, model) { AppKey = appKey});
                returnData.statusCode = StatusCodes.Status200OK;
                returnData.status = "success";
                returnData.message = "Information has been updated successfully";
                returnData.data = new Dictionary<string, object> { { "Did", role } };
                return StatusCode(StatusCodes.Status200OK, returnData);
            }
            else
            {
                returnData.statusCode = StatusCodes.Status200OK;
                returnData.status = "success";
                returnData.message = "Same role exist";
                returnData.data = new Dictionary<string, object> { { "Did", "" } };
                return StatusCode(StatusCodes.Status200OK, returnData);
            }
        }
        private async Task<bool> IsUserExist(string roleName, string did)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            bool exist = await _mediator.Send(new CheckRoleByQuery { Appkey = appKey, RoleName = roleName, Did = did });
            return exist;
        }

        [Authorize]
        [HttpGet]
        [Route("{did}")]
        public async Task<IActionResult> GetRoleByDidAsync(string did)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<Dictionary<string, object>>();

            var result = await _mediator.Send(new GetRoleByDid { AppKey = appKey, Did = did });

            returnData.statusCode = StatusCodes.Status200OK;
            returnData.status = "success";
            returnData.message = "";
            returnData.data = result;
            return StatusCode(StatusCodes.Status200OK, returnData);
        }
    }
}
