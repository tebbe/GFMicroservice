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
using System.Text;
using System.Security.Cryptography;
using DatabaseLayer.Utility;

namespace GroundFloor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSystemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IPremiseService _premiseService;
        private readonly IConfiguration _config;
        public UserSystemsController(IPremiseService premiseService, IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _premiseService = premiseService;
            _config = config;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAppsName")]
        public async Task<IActionResult> GetAsync()
        {
            var returnData = new ApiResponseSuccess<List<AppName>>();
            var arrayData = "[{\"label\":\"Adaptive Workflow\",\"value\":\"ChangeRequest\"}," +
                 "{\"label\":\"Asset Inventory\",\"value\":\"AssetInventory\"},{\"label\":\"BI Dashboard\",\"value\":\"BIDashboard\"}," +
                 "{\"label\":\"BSOM\",\"value\":\"BSOM\"},{\"label\":\"Baseline Equation\",\"value\":\"BaselineEquation\"}," +
                 "{\"label\":\"Capital Expense\",\"value\":\"CEM\"},{\"label\":\"CheckList\",\"value\":\"CheckList\"}," +
                 "{\"label\":\"Contact Center\",\"value\":\"ContactCenter\"},{\"label\":\"Dashboard\",\"value\":\"Dashboard\"}," +
                 "{\"label\":\"Data Operations Centre (DOC)\",\"value\":\"EnergyAdvantage\"}," +
                 "{\"label\":\"Data Operations Centre (DOC) V1\",\"value\":\"EnergyAdvantage\"}," +
                 "{\"label\":\"Event Management\",\"value\":\"EventManagement\"},{\"label\":\"Form Engine\",\"value\":\"FE\"}," +
                 "{\"label\":\"Ground Floor\",\"value\":\"Ground Floor\"},{\"label\":\"Hosted VDI Request Form\",\"value\":\"TDVDI\"}," +
                 "{\"label\":\"Lease Abstractor\",\"value\":\"LeaseAbstractor\"},{\"label\":\"Lease Flow\",\"value\":\"LeaseFlow\"}," +
                 "{\"label\":\"Lease View\",\"value\":\"StackingPlan\"},{\"label\":\"Operational Stack\",\"value\":\"OperationalStack\"}," +
                 "{\"label\":\"Performance Dashboard\",\"value\":\"PerformanceDashboard\"}," +
                 "{\"label\":\"Place Match\",\"value\":\"PlaceMatch\"},{\"label\":\"Report Engine\",\"value\":\"ReportEngine\"}," +
                 "{\"label\":\"Risk Management\",\"value\":\"RiskManagement\"},{\"label\":\"SirtNet Portal\",\"value\":\"SirtNetPortal\"}," +
                 "{\"label\":\"Storage UI\",\"value\":\"StorageUI\"},{\"label\":\"Support\",\"value\":\"Support\"}," +
                 "{\"label\":\"Tenant COI\",\"value\":\"COI\"},{\"label\":\"Tenant Request\",\"value\":\"TenantRequest\"}," +
                 "{\"label\":\"User System\",\"value\":\"UserSystem\"},{\"label\":\"Vendor Compliance\",\"value\":\"VendorCompliance\"}," +
                 "{\"label\":\"Welcome App\",\"value\":\"Welcome\"},{\"label\":\"Work Order Lite\",\"value\":\"WorkOrderLite\"}," +
                 "{\"label\":\"Work Orders\",\"value\":\"PreventiveMaintenance\"},{\"label\":\"Yumani\",\"value\":\"Yumani\"}]";
            var data = (List<AppName>)JsonConvert.DeserializeObject(arrayData, typeof(List<AppName>));

            returnData.statusCode = StatusCodes.Status200OK;
            returnData.status = "success";
            returnData.message = "";
            returnData.data = data;
            return StatusCode(StatusCodes.Status200OK, returnData);
        }

        [Authorize]
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<Dictionary<string, object>>();

            var data = await _mediator.Send(new GetUserByDidQuery { AppKey = appKey, UserId = userId });
            returnData.statusCode = StatusCodes.Status200OK;
            returnData.status = "success";
            returnData.message = data == null ? "No data found" : "";
            returnData.data = data;
            return StatusCode(StatusCodes.Status200OK, returnData);
        }

        [Authorize]
        [HttpPost]
        [Route("UserList")]
        public async Task<IActionResult> Post([FromQuery] UserListQueryModel queryModel, [FromQuery] Pagination pagination)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            string userID = identity.FindFirst("jid").Value;
            var returnData = new ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>();

            var data = await _mediator.Send(new GetUserListByQuery { AppKey = appKey, QueryParam = queryModel, Paging = pagination });

            var count = await _mediator.Send(new GetUserListTotalByQuery { AppKey = appKey, QueryParam = queryModel, Paging = pagination });
            returnData.statusCode = StatusCodes.Status200OK;
            returnData.status = "success";
            returnData.message = "";
            HttpContext.Response.Headers.Add("X-Total-Count", count.ToString());
            returnData.data = data;
            return StatusCode(StatusCodes.Status200OK, returnData);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] UserModel model)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            string userID = identity.FindFirst("jid").Value;
            var returnData = new ApiResponseSuccess<Dictionary<string, object>>();


            if (!IsUserExist(model.UserName, "").Result)
            {
                model.Password = Encrypt(model.Password);

                var did = await _mediator.Send(new InsertUserCommand(userID, model) { AppKey = appKey });

                if (!string.IsNullOrEmpty(did))
                {
                    await SaveOrUpdateUserInMicroServiceAsync(appKey, did, model);
                }
                returnData.statusCode = StatusCodes.Status201Created;
                returnData.status = "success";

                RoleUserMapping roleUserMapping = new RoleUserMapping();
                if (model.RoleList != null && model.RoleList.Length > 0)
                {
                    roleUserMapping.RoleID = string.Join(",", model.RoleList);
                }
                else
                {
                    roleUserMapping.RoleID = string.Empty;
                }

                roleUserMapping.UserID = did;
                var result = await _mediator.Send(new InsertRoleUserMappingCommand(userID, roleUserMapping) { AppKey = appKey });

                returnData.message = "Information has been saved successfully";
                returnData.data = new Dictionary<string, object> { { "Did", did } };

                return StatusCode(StatusCodes.Status201Created, returnData);
            }
            else
            {
                returnData.statusCode = StatusCodes.Status200OK;
                returnData.status = "success";
                returnData.message = "User already exist";
                returnData.data = new Dictionary<string, object> { { "Did", "" } };
                return StatusCode(StatusCodes.Status200OK, returnData);
            }

        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UserModel model)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            string userID = identity.FindFirst("jid").Value;
            var returnData = new ApiResponseSuccess<Dictionary<string, object>>();

            if (!IsUserExist(model.UserName, model.Did).Result)
            {
                if (!string.IsNullOrEmpty(model.Password))
                {
                    model.Password = Encrypt(model.Password);
                }
                var isUpdate = await _mediator.Send(new UpdateUserCommand(userID, model) { AppKey = appKey });


                if (isUpdate)
                {
                    await SaveOrUpdateUserInMicroServiceAsync(appKey, model.Did, model, "update");
                }

                returnData.statusCode = StatusCodes.Status201Created;
                returnData.status = isUpdate ? "success" : "fail";
                returnData.message = isUpdate ? "Information has been update successfully" : "No user Found";
                returnData.data = new Dictionary<string, object> { { "Did", model.Did } };

                if (returnData.status == "success")
                {
                    var deleteRoleUserMapping = await _mediator.Send(new DeleteRoleUserMappingCommand { AppKey = appKey, UserID = model.Did });

                    RoleUserMapping roleUserMapping = new RoleUserMapping();

                    if (model.RoleList != null && model.RoleList.Length > 0)
                        roleUserMapping.RoleID = string.Join(",", model.RoleList);
                    else
                        roleUserMapping.RoleID = string.Empty;


                    roleUserMapping.RoleID = string.Join(",", model.RoleList);
                    roleUserMapping.UserID = model.Did;
                    var result = await _mediator.Send(new InsertRoleUserMappingCommand(userID, roleUserMapping) { AppKey = appKey });
                }

                return StatusCode(StatusCodes.Status201Created, returnData);
            }
            else
            {
                returnData.statusCode = StatusCodes.Status200OK;
                returnData.status = "success";
                returnData.message = "User already exist";
                returnData.data = new Dictionary<string, object> { { "Did", "" } };
                return StatusCode(StatusCodes.Status200OK, returnData);
            }

        }

        private async Task<bool> IsUserExist(string userName, string did)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            bool exist = await _mediator.Send(new CheckUserByQuery { Appkey = appKey, UserName = userName, Did = did });

            return exist;
        }

        private async Task SaveOrUpdateUserInMicroServiceAsync(string appKey, string userID, UserModel model, string type = "insert")
        {
            string data = JsonConvert.SerializeObject(new
            {
                UserId = userID,
                AppKey = appKey,
                UserName = model.UserName,
                Password = string.IsNullOrEmpty(model.Password) ? "" : model.Password,
                Type = "User",
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsActive = "1",
                IsApprove = model.IsApprove
            });

            var response = new Dictionary<string, object>();
            var tokenRaw = await _premiseService.GetApiTokenAsync(appKey);
            string token = "Bearer " + tokenRaw;
            switch (type)
            {
                case "insert":
                    {

                        string apiUrl = _config.GetValue<string>("ApiDomain") + "/accounts/v2/premiseusers";
                        response = await _premiseService.PostDataFromApiAsync<Dictionary<string,object>>(token, apiUrl, data);
                        break;
                    }
                case "update":
                    {
                        string apiUrl = _config.GetValue<string>("ApiDomain") + "/accounts/v2/premiseusers/" + userID;
                        using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, apiUrl))
                        {
                            requestMessage.Headers.Authorization =
                                new AuthenticationHeaderValue("Bearer", tokenRaw);

                            requestMessage.Content = new StringContent(data, Encoding.UTF8, "application/json");
                            var _client = new HttpClient();
                            var result = await _client.SendAsync(requestMessage);
                            var jsonString = await result.Content.ReadAsStringAsync();
                            JObject m = JObject.Parse(jsonString);
                        }
                        break;
                    }

            }


        }

        #region responsibility region-assign START
        [Authorize]
        [HttpPost]
        [Route("responsibility")]
        public async Task<IActionResult> Post([FromBody] ReponsibilityQuery reponsibility, [FromQuery] Pagination pagination)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            string userID = identity.FindFirst("jid").Value;
            var returnData = new ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>();

            var data = await _mediator.Send(new ResponsibilityReAssignQuery { AppKey = appKey, Search = reponsibility.Search,UserType=reponsibility.UserType, Did = reponsibility.Did, Pagination = pagination });
            returnData.statusCode = StatusCodes.Status200OK;
            returnData.status = "success";
            returnData.message = "";
            returnData.data = data;

            return StatusCode(StatusCodes.Status200OK, returnData);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            string userID = identity.FindFirst("jid").Value;
            var returnData = new ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>();

            var data = await _mediator.Send(new ResponsibilityIntegrationQuery { AppKey = appKey });
            returnData.statusCode = StatusCodes.Status200OK;
            returnData.status = "success";
            returnData.message = "";
            returnData.data = data;

            return StatusCode(StatusCodes.Status200OK, returnData);
        }

        #endregion END

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "c0d3funrdowlaqw";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        #region permission

        [Authorize]
        [HttpGet]
        [Route("permission/{roleid}/{appid?}")]
        public async Task<IActionResult> Get(string roleid, string appid = "")
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var appKey = identity.FindFirst("jak").Value;
            var returnData = new ApiResponseSuccess<ReturnRoleData>();

            var data = await _mediator.Send(new GetPermissionByRoleIdAndAppId { AppKey = appKey, RoleId = roleid, AppDid = appid });
            returnData.statusCode = StatusCodes.Status200OK;
            returnData.status = "success";
            returnData.message = "";
            returnData.data = data;
            return StatusCode(StatusCodes.Status200OK, returnData);
        }

        [Authorize]
        [HttpPost]
        [Route("permissionmapping")]
        public async Task<IActionResult> Post([FromBody] PermissionMappingQuery model)
        {
            string did = "";
            bool isExist = true;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var appKey = identity.FindFirst("jak").Value;
            string userID = identity.FindFirst("jid").Value;
            var returnData = new ApiResponseSuccess<Dictionary<string, object>>();
            RolePermissionMappingModel modeData = new RolePermissionMappingModel();
            var data = await _mediator.Send(new GetPermissionByRoleId { AppKey = appKey, RoleId = model.Role });
            if(data==null || data["UserPermission"] == null) { isExist = false; }
            
            if (!isExist)
            {

                modeData.Role = model.Role;
                modeData.UserPermission = model.UserPermission;
                await _mediator.Send(new InsertRolePermissionMappingCommand(userID, modeData) { AppKey = appKey });
                did = modeData.Did;
            }
            else
            {
                var json = JsonConvert.SerializeObject(data);
                modeData = JsonConvert.DeserializeObject<RolePermissionMappingModel>(json);
                modeData.UserPermission = string.IsNullOrEmpty(model.UserPermission) ? modeData.UserPermission : model.UserPermission;
                await _mediator.Send(new UpdateRolePermissionMappingCommand(userID, modeData) { AppKey = appKey });
                did = data["Did"].ToString();
            }

            returnData.statusCode = StatusCodes.Status200OK;
            returnData.status = "success";
            returnData.message = "";
            returnData.data = new Dictionary<string, object> { { "Did", did } }; ;

            return StatusCode(StatusCodes.Status200OK, returnData);
        }

        [Authorize]
        [HttpPost]
        [Route("Usersappsbypermissions")]
        public async Task<IActionResult> Post([FromBody] UsersAppsPermissionsQuery queryString)
        {
            bool isCompleted = false;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var appKey = identity.FindFirst("jak").Value;
            string userID = identity.FindFirst("jid").Value;
            var returnData = new ApiResponseSuccess<Dictionary<string, object>>();

            if (!string.IsNullOrEmpty(queryString.Role) && queryString.UserPermission.Length > 0)
            {
                isCompleted = await _mediator.Send(new UsersAppsByPermissionsQuery { AppKey = appKey, UserId = userID, RoleDid = queryString.Role.Split(","), UserPermission = queryString.UserPermission });
            }

            returnData.statusCode = StatusCodes.Status200OK;
            returnData.status = "success";
            returnData.message = "";
            returnData.data = new Dictionary<string, object> { { "Completed", isCompleted } }; ;

            return StatusCode(StatusCodes.Status200OK, returnData);
        }


        private async Task<Dictionary<string, object>> GetRoleByDid(string did)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string appKey = identity.FindFirst("jak").Value;
            var result = await _mediator.Send(new GetRoleByDid { AppKey = appKey, Did = did });


            return result;
        }

        #endregion permission

    }
}

