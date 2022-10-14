using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GFTeamsModel
    {
        public string Did {get;set;}
        public string TeamName { get; set; }
        public string TeamColor { get; set; }
        public string WorkingDays { get; set; }
        public string AlternateByWeek { get; set; }
        public bool ActiveStatus { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime InsertedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
