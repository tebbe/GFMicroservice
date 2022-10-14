using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.QueryString
{
    public class PermissionMappingQuery
    {
        [Required]
        public string Role { get; set; }
        public string UserPermission { get; set; }
    }
}
