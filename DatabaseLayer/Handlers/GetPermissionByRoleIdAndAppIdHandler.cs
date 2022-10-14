using DatabaseLayer.Dal;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.Extensions.Options;
using Model;
using Model.QueryString;
using Model.UserSystem;
using PremiseGlobalLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseLayer.Handlers
{
    public class GetPermissionByRoleIdAndAppIdHandler : IRequestHandler<GetPermissionByRoleIdAndAppId, ReturnRoleData>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private IUserRolesDal _userRolesDal;
        private UserPermissionDal _userPermissionDal;
        private IUserDal _userDal;
        public GetPermissionByRoleIdAndAppIdHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, IUserRolesDal userRoleDal, UserPermissionDal userPermissionDal, IUserDal userDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userPermissionDal = userPermissionDal;
            _userDal = userDal;
            _userRolesDal = userRoleDal;
        }

        public async Task<ReturnRoleData> Handle(GetPermissionByRoleIdAndAppId request, CancellationToken cancellationToken)
        {
            string userPermission = "";
            string userPermissionDid = "";
            List<UserPermission> permissionList = new List<UserPermission>();

            var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
            string roleCollection = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.RoleModel);
            var userRole = await _userRolesDal.GetRoleByDidAsync(roleCollection, request.RoleId);
            
            string premissionCollection = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.UserPermission);
            var userPremission = await _userPermissionDal.GetPermissionAsync(premissionCollection, new UserPermissionQueryModel { AppId = request.AppDid }, new Pagination { Skip = 0, Limit = 100 });

            string rolePreMappingCollection = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.RolePermissionMapping);
            var rolePermissionMapping = await _userDal.GetRolePermissionMapping(rolePreMappingCollection, request.RoleId);

            if (rolePermissionMapping != null)
            {
                userPermission = rolePermissionMapping["UserPermission"] != null ? rolePermissionMapping["UserPermission"].ToString() : "";
                userPermissionDid = rolePermissionMapping["Did"].ToString();

            }
            string[] userPermissionArray = userPermission.Split(',');

            for (int i = 0; i < userPremission.ToList().Count; i = i + 2)
            {
                string did1 = "";
                string permission1 = "";
                string did2 = "";
                string permission2 = "";
                string AppId1 = "";
                string AppId2 = "";
                bool flag1 = false;
                bool flag2 = false;
                try
                {
                    did1 = userPremission.ToList()[i]["Did"].ToString();
                    permission1 = userPremission.ToList()[i]["PermissionName"].ToString();
                    int check = Array.IndexOf(userPermissionArray.ToArray(), did1);
                    AppId1 = userPremission.ToList()[i]["AppId"].ToString();

                    if (check >= 0)
                        flag1 = true;
                }
                catch { }

                try
                {
                    did2 = userPremission.ToList()[i + 1]["Did"].ToString();
                    permission2 = userPremission.ToList()[i + 1]["PermissionName"].ToString();
                    int check = Array.IndexOf(userPermissionArray.ToArray(), did2);
                    AppId2 = userPremission.ToList()[i + 1]["AppId"].ToString();

                    if (check >= 0)
                        flag2 = true;
                }
                catch (Exception ex)
                {
                    var mess = ex.Message;
                }

                permissionList.Add(new UserPermission()
                {
                    AppId1 = AppId1,
                    AppId2 = AppId2,
                    Did1 = did1,
                    Permission1 = permission1,
                    flag1 = flag1,
                    Did2 = did2,
                    Permission2 = permission2,
                    flag2 = flag2
                });
            }

            ReturnRoleData obj = new ReturnRoleData();
            obj.Permission = permissionList;
            obj.UserPermission = userPermission;
            obj.UserPermissionDid = userPermissionDid;

            return obj;
        }
    }
}
