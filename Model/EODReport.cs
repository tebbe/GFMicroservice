using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EODReport
    {
        public string Type { get; set; }
        public string TotalInOut { get; set; }
        public string Actions { get; set; }
        public string Avgoccupancy { get; set; }
        public string Building { get; set; }
        public string BuildingTotalIn { get; set; }
        public string BuildingTotalOut { get; set; }
        public string Coefficent { get; set; }
        public string EndDate { get; set; }
        public string EntranceBySensor { get; set; }
        public string HighRiseTotalIn { get; set; }
        public string HighRiseTotalOut { get; set; }
        public string LowRiseTotalIn { get; set; }
        public string LowRiseTotalOut { get; set; }
        public string Other { get; set; }
        public string PeakActivity { get; set; }
        public string PeakActivityTime { get; set; }
        public string PeakOccupancy { get; set; }
        public string PeakOccupancyTime { get; set; }
        public string StartDate { get; set; }
        public string TotalIn { get; set; }
        public string TotalOut { get; set; }
        public string TowerAverage { get; set; }
        public string TowerEntranceTotalIn { get; set; }
        public string TowerEntranceTotalOut { get; set; }
    }
}
