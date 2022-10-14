using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GFResourcesFlatModel
    {
        public string Did { get; set; }
        public string Serial { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string ZoneID { get; set; }
        public string TeamDid { get; set; }
        public string TeamName { get; set; }
        public string TeamColor { get; set; }
        public string WorkingDays { get; set; }
        public string ResourceDid { get; set; }
        public string ResourceName { get; set; }
        public string BuildingId { get; set; }
        public string FloorId { get; set; }
        public string LayerId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime InsertedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
