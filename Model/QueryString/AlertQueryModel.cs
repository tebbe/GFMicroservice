using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.QueryString
{
    public class AlertQueryModel
    {
        [Required]
        public string[] BuildingIds { get; set; }
        public string Floorid { get; set; }
        public string Resolved { get; set; }
        public string Severity { get; set; }

    }
}
