using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EODRunning
    {
        public string Building { get; set; }
        public string PeakActivity { get; set; }
        public string PeakActivityTime { get; set; }
        public string PeakOccupancy { get; set; }
        public string PeakOccupancyTime { get; set; }
        public string CurrentMonthDate { get; set; }
    }
}
