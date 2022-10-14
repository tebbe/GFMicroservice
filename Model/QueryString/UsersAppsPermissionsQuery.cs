using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.QueryString
{
    public class UsersAppsPermissionsQuery  
    {
        [Required]
        public string Role { get; set; }
        [Required]
        public string[] UserPermission { get; set; }
    }
}
