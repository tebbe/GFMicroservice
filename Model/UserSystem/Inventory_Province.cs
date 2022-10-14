using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserSystem
{
    public class Inventory_Province
    {
        public string Did { get; set; }
        public string Province { get; set; }
        public string Code { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
