using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserSystem
{
    public class RoleModel
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string AppId { get; set; }
        public string Did { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? InsertedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        


    }
}
