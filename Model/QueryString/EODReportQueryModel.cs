using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.QueryString
{
    public class EODReportQueryModel
    {
        [Required]
        public string BuildingId { get; set; }

        [Required]
        public string Type { get; set; }

        [MaxLength(20)]
        public string[] Others { get; set; }   

        
    }
}
