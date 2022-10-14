using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserSystem
{
    public class PropertyBuilding
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string BuildingName { get; set; }
        public string BuildingId { get; set; }
        public string FloorId { get; set; }
        public string FloorName { get; set; }
        public string SuiteId { get; set; }
        public string SuiteName { get; set; }
    }
    public class UserModel
    {
        public string  Did { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string IsVendor { get; set; } = "false";
        public string UserName { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PropertyBuilding { get; set; }
        public string PhoneWork { get; set; }
        public string PhoneMobile { get; set; }
        public string EmployeeID { get; set; }
        public string Rate { get; set; }
        public string IsCompanyOwner { get; set; } = "false";
        public string AutoLoginEnable { get; set; } = "true";
        public string IsInitialPasswordChange { get; set; } = "false";
        public string Language { get; set; }
        public string IsApprove { get; set; } = "1";
        public string Photo { get; set; }
        public string IsNewUser { get; set; } = "1";
        public string UserType { get; set; }
        public string Position { get; set; }
        public string[] RoleList { get; set; }
        public string RateType { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Zip { get; set; }
        public string AssetClass { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string CreatedBy { get; set; }
        public DateTime InsertedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [NotMapped]
        public string PhotoPath { get; set; }
    }
}
