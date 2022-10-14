using DatabaseLayer.Commands;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Model.UserSystem;
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
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IPremiseService _premiseService;

        public PropertiesController(IPremiseService premiseService, IMediator mediator)
        {
            _mediator = mediator;
            _premiseService = premiseService;
        }

        [Authorize]
        [Route("{name}/propertywithbuilding")]
        [HttpPost]
        public async Task<IActionResult> GetPropertyWithBuildingAsync(string name)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;

            var returnData = new ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>();

            var result = await _mediator.Send(new GetPropertyWithBuilding { AppKey = appKey, Name = name });
            
            returnData.data = result;           
            returnData.status = "success";
            returnData.statusCode = StatusCodes.Status200OK;
            return Ok(returnData);
        }

    }
}
