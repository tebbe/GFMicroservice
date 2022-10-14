using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserSystem
{
    public class UserAppByPermissionModel
    {
        public string Did { get; set; }
        public string AppId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionKey { get; set; }
    }
}
