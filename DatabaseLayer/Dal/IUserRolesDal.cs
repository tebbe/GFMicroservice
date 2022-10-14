using DatabaseLayer.Queries;
using Model.QueryString;
using Model.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Dal
{
    public interface IUserRolesDal
    {
        
        Task<string> SaveAsync(string collectionName, RoleModel roleModel);
        Task<string> UpdateAsync(string collectionName,  RoleModel roleModel);
        Task<bool> IsExist(string collectionName, string roleName, string did);
        Task<IEnumerable<Dictionary<string, object>>> GetRoleAsync(string collectionName, GetAllUserRoles querymodel);

        Task<long> GetUserRoleListTotalCount(string collectionName, GetUserRolesListTotalByQuery request);

        Task<Dictionary<string, object>> GetRoleByDidAsync(string collectionName, string did);

    }
}
