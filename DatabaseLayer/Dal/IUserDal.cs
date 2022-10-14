using DatabaseLayer.Commands;
using Model;
using DatabaseLayer.Queries;
using Model;
using Model.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Dal
{
    public interface IUserDal
    {
        Task<Dictionary<string, object>> GetByIdAsync(string collectionName, string did);    
        Task<IEnumerable<Dictionary<string, object>>> GetUserList(string collectionName, GetUserListByQuery query);
        Task<long> GetUserListTotalCount(string collectionName, GetUserListTotalByQuery query);
        Task<string> SaveAsync(string collectionName, UserModel model);
        Task<bool> UpdateAsync(string collectionName, UserModel model);
        Task<bool> DeleteAsync(string collectionName, string did);
        Task<bool> IsExist(string collectionName, string userName,string did);
        Task<IEnumerable<Dictionary<string, object>>> GetResponsibilityReAssign(string collectionName, ResponsibilityReAssignQuery request);
        Task<IEnumerable<Dictionary<string, object>>> GetAPIIntegration(string collectionName);
        Task<Dictionary<string, object>> GetRolePermissionMapping(string collectionName,string roleDid);
        Task<bool> SaveRolePermissionMapping(string collectionName, RolePermissionMappingModel model);
        Task<bool> UpdateRolePermissionMapping(string collectionName, RolePermissionMappingModel model);

        Task<IEnumerable<Dictionary<string, object>>> GetRoleUserMapping(string collectionName, string[] roleDid);
        Task<IEnumerable<Dictionary<string, object>>> GetRolePermissionUserMapping(string collectionName, string[] roleDid);
        Task<IEnumerable<Dictionary<string, object>>> GetUserPermissionAsync(string collectionName, string[] permissionDid);
        Task<IEnumerable<Dictionary<string, object>>> GetUserAsync(string collectionName, string[] userIds);    
        Task<bool> BulkInsertToUserPermission(string collectionName,List<Dictionary<string,object>> dataList);
        Task<IEnumerable<Dictionary<string, object>>> GetpremiseUserMapping(string roleUserCollection, string[] usersDid);
        Task <bool> DeleteUserPermission(string userpermissionCollection, string[] userDids, string[] appDids); 
    }
    
}
