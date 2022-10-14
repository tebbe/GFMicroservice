using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.QueryString
{
    public class UserListQueryModel
    {
        public string Search { get; set; }
        public string UserType { get; set; }
        public string IsApprove { get; set; } = "1";       
    }
}
