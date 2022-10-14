using Model;
using Model.QueryString;
using Model.UserSystem;
using Newtonsoft.Json;
using PremiseGlobalLibrary.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GroundFloor.Tests
{
    public class UserSystemControllerTest
    {
        public Helper helper = new Helper();

        [Fact]
        public async Task Post()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            string[] roleList = { "User", "Test" };
            var datalist = "[{\"PropertyId\":\"0257e39d088744fca69cc1c13031c01a\",\"PropertyName\":\"100LOMB\",\"BuildingName\":\"179 John Street\",\"BuildingId\":\"942f421efb164e478e05188d8d8c19c2\"},{\"PropertyId\":\"0257e39d088744fca69cc1c13031c01a\",\"PropertyName\":\"100LOMB\",\"BuildingName\":\"test\",\"BuildingId\":\"02e59fd610f343f89d558bb718777094\"}]";
            UserModel userModel = new UserModel()
            {
                Did = Guid.NewGuid().ToString("N"),
                Email = "vorcpsu01@enayu.com",
                IsVendor = "false",
                UserName = "vorkpsu01@enayu.com",
                Title = "test",
                FirstName = "FirstTest",
                LastName = "name",
                PhoneWork = "0923409234",
                PhoneMobile = "2343432",
                EmployeeID = "se333",
                Rate = "4",
                IsCompanyOwner = "false",
                AutoLoginEnable = "true",
                IsInitialPasswordChange = "true",
                Language = "EN",
                IsApprove = "true",
                IsNewUser = "true",
                UserType = "user",
                Position = "test",
                RoleList = roleList,
                RateType = "test",
                City = "test",
                Address = "test",
                Province = "test",
                Zip = "a3as456",
                AssetClass = "test",
                Password = "D@fu8sdf8df",
                PropertyBuilding = datalist,
                Photo = "download.jpg",
                InsertedDate = DateTime.UtcNow,
                UpdatedBy = "",
                CreatedBy = "c8f8fdaf4887459c9c0dcb60fac976e2",
                UpdatedDate = null

            };

            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/UserSystems", JsonConvert.SerializeObject(userModel));
            Assert.Equal(201, data.statusCode);
            Assert.Equal(200, data.statusCode);

        }
        [Fact]
        public async Task Put()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var datalist ="[{\"PropertyId\":\"0257e39d088744fca69cc1c13031c01a\",\"PropertyName\":\"100LOMB\",\"BuildingName\":\"179 John Street\",\"BuildingId\":\"942f421efb164e478e05188d8d8c19c2\"},{\"PropertyId\":\"0257e39d088744fca69cc1c13031c01a\",\"PropertyName\":\"100LOMB\",\"BuildingName\":\"test\",\"BuildingId\":\"02e59fd610f343f89d558bb718777094\"}]";
            string[] roleList = { "User", "Test" };

            UserModel userModel = new UserModel()
            {
                Email = "vorcpsu01@enayu.com",
                IsVendor = "false",
                UserName = "vorkpsu01@enayu.com",
                Title = "test",
                FirstName = "FirstTest",
                LastName = "name",
                PhoneWork = "0923409234",
                PhoneMobile = "2343432",
                EmployeeID = "se333",
                Rate = "4",
                IsCompanyOwner = "false",
                AutoLoginEnable = "true",
                IsInitialPasswordChange = "true",
                Language = "EN",
                IsApprove = "true",
                IsNewUser = "true",
                UserType = "user",
                Position = "test",
                RoleList = roleList,
                RateType = "test",
                City = "test",
                Address = "test",
                Province = "test",
                Zip = "a3as456",
                AssetClass = "test",
                Password = "D@fu8sdf8df",
                PropertyBuilding = datalist,
                Photo = "30072022054947_download.jpg",
                Did = "efa44c3de865447f89e04e8af0b349ca",
                UpdatedBy = "c8f8fdaf4887459c9c0dcb60fac976e2",
                UpdatedDate = DateTime.UtcNow
            };

            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/UserSystems", JsonConvert.SerializeObject(userModel));
            Assert.Equal(201, data.statusCode);
            Assert.Equal(200, data.statusCode);

        }

        [Fact]
        public async Task GetUserList()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/UserSystems/UserList?Search=tasnim@gmail.com&UserType=1&Skip=0&Limit=50");
            Assert.Equal(200, data.statusCode);
        }

        [Fact]
        public async Task GetUserByUserID()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.GetDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/UserSystems/efa44c3de865447f89e04e8af0b349ca");
            Assert.Equal(200, data.statusCode);
        }

        [Fact]
        public async Task GetAppList()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.GetDemoFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/UserSystems/GetAppsName");
            Assert.Equal(200, data);
        }

        [Fact]
        public async Task GetResponsibility()
        {
            var dataParam = new ReponsibilityQuery
            {
                Search = "LeaseAdmin",
                UserType="1"
            };
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>>(token, "https://localhost:44347/api/UserSystems/responsibility?Skip=0&Limit=5", JsonConvert.SerializeObject(dataParam));
            Assert.Equal(200, data.statusCode);
        }
        [Fact]
        public async Task GetResponsibilityWithDId()
        {
            var dataParam = new ReponsibilityQuery
            {
                Search = "LeaseAdmin",
                UserType = "1",
                Did = "3a05efdad5ad483db20a4d14f9e9d742"
            };
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>>(token, "https://localhost:44347/api/UserSystems/responsibility?Skip=0&Limit=5", JsonConvert.SerializeObject(dataParam));
            Assert.Equal(200, data.statusCode);
        }

        [Fact]
        public async Task GetIntegration()
        {
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.GetDataFromApiAsync<ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>>(token, "https://localhost:44347/api/UserSystems");
            Assert.Equal(200, data.statusCode);
        }

        [Fact]
        public async Task GetPermission()
        {
           
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.GetDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/UserSystems/permission/630d087b420b4424a50ce0cb3c00fc98");
            Assert.Equal(200, data.statusCode);
        }

        [Fact]
        public async Task SaveAndUpdatePermissionMapping()
        {
            var dataParam = new PermissionMappingQuery
            {
                Role = "1a30e6e99e8f414798c37b9f162c5820",
                UserPermission = "6b05c9c156094299bd430da90f65592c,04d94f5472a049a8aabf5eaaddce2807"
            };
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/UserSystems/permissionmapping", JsonConvert.SerializeObject(dataParam));
            Assert.Equal(200, data.statusCode);
        }


        [Fact]
        public async Task UsersAppsByPermissions()
        {
            var dataParam = new UsersAppsPermissionsQuery
            {
                Role = "630d087b420b4424a50ce0cb3c00fc98",
                UserPermission = new string[] { "6b05c9c156094299bd430da90f65592c","04d94f5472a049a8aabf5eaaddce2807","a5e9536b350945bf9da3d1c192a4c9f6","d64b4975d47a41efb21261b7ab7d5018","666dec0d6be04d8fad78fa27bf7bc53e" }
            };
            var d = JsonConvert.SerializeObject(dataParam);
            string token = "Bearer " + await helper.GetTokenAsync();
            var data = await helper.PostDataFromApiAsync<ApiResponseSuccess<Dictionary<string, object>>>(token, "https://localhost:44347/api/UserSystems/Usersappsbypermissions", JsonConvert.SerializeObject(dataParam));
            Assert.Equal(200, data.statusCode);
        }
    }
}
