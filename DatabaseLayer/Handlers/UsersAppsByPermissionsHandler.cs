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
    public class UsersAppsByPermissionsHandler : IRequestHandler<UsersAppsByPermissionsQuery, bool>
    {
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;
        private IUserRolesDal _userRolesDal;
        private UserPermissionDal _userPermissionDal;
        private IUserDal _userDal;
        public UsersAppsByPermissionsHandler(IPremiseService premiseService, IOptions<DBCollections> dbCollections, IUserRolesDal userRoleDal, UserPermissionDal userPermissionDal, IUserDal userDal)
        {
            _premiseService = premiseService;
            _dbCollections = dbCollections;
            _userPermissionDal = userPermissionDal;
            _userDal = userDal;
            _userRolesDal = userRoleDal;
        }

        public async Task<bool> Handle(UsersAppsByPermissionsQuery request, CancellationToken cancellationToken)
        {
            List<string> strUsers =new List<string>();
            List<string> strPermissions = new List<string>();

            var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
            string roleUserCollection = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.PremiseRoleUserMapping);

            var roleUserMapping = await _userDal.GetRoleUserMapping(roleUserCollection, request.RoleDid);

            foreach (Dictionary<string, object> item in roleUserMapping)
            {
                strUsers.Add(item["UserID"].ToString());
            }

            string rolePreMappingCollection = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.RolePermissionMapping);
            var rolePermissionMapping = await _userDal.GetRolePermissionUserMapping(rolePreMappingCollection, request.RoleDid);

            foreach (Dictionary<string, object> item in rolePermissionMapping)
            {
                string tempPermission = item["UserPermission"].ToString();
                foreach (string str in tempPermission.Split(','))
                {
                    strPermissions.Add(str);
                }
            }
            string premissionCollection = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.UserPermission);
            var userPremission = await _userDal.GetUserPermissionAsync(premissionCollection, strPermissions.ToArray());


            string userCollection = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.UserSystem);
            var userList = await _userDal.GetUserAsync(userCollection, strUsers.ToArray());

            //delete start
            var delUserPremission = await _userDal.GetUserPermissionAsync(premissionCollection, request.UserPermission);
            if (strUsers.Count > 0)
            {
                foreach (string user in strUsers)
                {
                    List<string> struser=new List<string>();
                    List<string> strApps = new List<string>();
                    List<string> strRoles = new List<string>();
                    
                    struser.Add(user);

                     var preUserMapping = await _userDal.GetpremiseUserMapping(roleUserCollection, struser.ToArray());

                    foreach (Dictionary<string, object> dr in preUserMapping)
                    {
                        if (!request.RoleDid.Contains(dr["RoleID"].ToString()))
                        {
                            strRoles.Add(dr["RoleID"].ToString());
                        }
                    }

                    var rolePermission2Mapping = await _userDal.GetRolePermissionUserMapping(rolePreMappingCollection, strRoles.ToArray());

                    List<string> strPermissions2 =new List<string>();
                    foreach (Dictionary<string, object> dr in rolePermission2Mapping)
                    {
                        string tempPermission = dr["UserPermission"].ToString();
                        foreach (string str in tempPermission.Split(','))
                        {
                           if(!str.Equals("0")) strPermissions2.Add(str); 
                        }
                    }
                    var userPremission2 = await _userDal.GetUserPermissionAsync(premissionCollection, strPermissions2.ToArray());


                    foreach (Dictionary<string, object> row in delUserPremission)
                    {
                        bool isExist = false;
                        foreach (Dictionary<string, object> Xrow in userPremission2)
                        {
                            if (row["AppId"].ToString().Equals(Xrow["AppId"].ToString()))
                            {
                                isExist = true;
                                break;
                            }
                        }

                        if (!isExist)
                        {
                            strApps.Add(row["AppId"].ToString());
                        }
                    }

                    string userpermissionCollection = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.User_Permission);
                    var permissionDelete = await _userDal.DeleteUserPermission(userpermissionCollection, struser.ToArray(), strApps.ToArray());

                }
            }
            //delete end
            //insert
            List<Dictionary<string, object>> listModelData = new List<Dictionary<string, object>>();

            foreach (Dictionary<string, object> dr in userList)
            {
                

                foreach (Dictionary<string, object> drApp in userPremission)
                {
                    Dictionary<string, object> model = new Dictionary<string, object>();
                    model.Add("Did", Guid.NewGuid().ToString("N"));
                    model.Add("UserId", dr["Did"].ToString());
                    model.Add("RoleId", 3);
                    model.Add("AppId", drApp["AppId"].ToString());
                    model.Add("InsertedDate", DateTime.UtcNow);
                    model.Add("CreatedBy", request.UserId);
                    listModelData.Add(model);
                }
               
            }

            string userpermission2Collection = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.User_Permission);
            var permission = listModelData.Count>0? await _userDal.BulkInsertToUserPermission(userpermission2Collection, listModelData):false;

            return permission;
        }
    }
}
