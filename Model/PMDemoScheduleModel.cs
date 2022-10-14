using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PMDemoScheduleModel
    {
        public string Did { get; set; }
        public string ScheduleID { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime InsertedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
