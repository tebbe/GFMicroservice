using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserSystem
{
    public class RolePermissionMappingModel
    {
        public string Did { get; set; } 
        public string Role { get; set; }    
        public string UserPermission { get; set; }  
        public string CreatedBy { get; set; }  
        public DateTime InsertedDate { get; set; }  
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
