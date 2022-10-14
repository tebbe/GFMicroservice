using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.QueryString
{
    public class PermissionQueryModel
    {
        [Required]
        public string RoleId { get; set; }
        public string AppDid { get; set; }  
    }
}
