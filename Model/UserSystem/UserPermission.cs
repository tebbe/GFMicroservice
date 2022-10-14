using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserSystem
{
    public class UserPermission
    {
		public string Did1 { get; set; }
		public string Permission1 { get; set; }
		public string Did2 { get; set; }
		public string Permission2 { get; set; }
		public bool flag1 { get; set; }
		public bool flag2 { get; set; }
		public string AppId1 { get; set; }
		public string AppId2 { get; set; }
	}
	public class ReturnRoleData
	{
		public List<UserPermission> Permission { get; set; }
		public string UserPermission { get; set; }
		public string UserPermissionDid { get; set; }
	}
}
