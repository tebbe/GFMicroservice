using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserPermissionModel
    {
        public string Did { get; set; }
        public string AppId { get; set; }
        public string Description { get; set; }
        public string PermissionKey { get; set; }
        public string PermissionName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime InsertedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
