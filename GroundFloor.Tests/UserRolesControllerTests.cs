using DatabaseLayer.Queries;
using GroundFloor.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Model.QueryString;
using Model.UserSystem;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PremiseGlobalLibrary;
using PremiseGlobalLibrary.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GroundFloor.Tests
{
    public class UserRolesControllerTests
    {
        
       
        public Helper helper = new Helper();
        [Fact]
        public async Task GetRoleAsync()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            //string format = "GF Forms Setting";
            var data = await helper.GetDataFromApiAsync<ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>>(token, "https://localhost:44347/api/UserSystemRoles?RoleName=Tenant");
            Assert.Equal(200, data.statusCode);
        }
        [Fact]
        public async Task PostRoleAsync()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var id = Guid.NewGuid().ToString("N");
                RoleModel roleModel = new RoleModel()
                {
                    AppId = "update demo 1",
                    RoleName = "Role update",
                    Description = "Demo",
                    Did = id,
                    CreatedBy = "",
                    InsertedDate = DateTime.Now,
                    UpdatedBy = "string",
                    UpdatedDate = DateTime.Now,
                };
            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess< Dictionary<string, object>>>(token, "https://localhost:44347/api/UserSystemRoles", JsonConvert.SerializeObject(roleModel));
            Assert.Equal(200, data.statusCode);
        }

        [Fact]
        public async Task PutRoleAsync()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            
            RoleModel roleModel = new RoleModel()
            {
                AppId = "update demo",
                RoleName = "Role update",
                Description = "Demo",
                
                CreatedBy = "",
                InsertedDate = DateTime.Now,
                UpdatedBy = "",
                UpdatedDate = DateTime.Now,
            };
            var data = await helper.PutDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/UserSystemRoles", JsonConvert.SerializeObject(roleModel));
            Assert.Equal(200, data.statusCode);
        }

        [Fact]
        public async Task GetRoleByDidAsync()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.GetDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/usersystemroles/9bc210cb4234482db0d4a73b98f6f66e");
            Assert.Equal(200, data.statusCode);
        }
    }
}
