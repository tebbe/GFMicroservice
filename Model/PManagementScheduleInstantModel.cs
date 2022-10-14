using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PManagementScheduleInstantModel
    {
        public string Did { get; set; }
        public string EquipmentsDetails { get; set; }
        public string IsStandard { get; set; }
        public string StandardText { get; set; }
        public string StandardID { get; set; }
        public string Repeats { get; set; }
        public string CustomTag { get; set; }
        public string DispatchUserInfo { get; set; }
        public string Date { get; set; }
        public string ScheduleID { get; set; }
        public string ScheduleName { get; set; }
        public string TaskID { get; set; }
        public string TaskName { get; set; }
        public string ModuleID { get; set; }
        public string ModuleDataDID { get; set; }
        public string PropertyID { get; set; }
        public string PropertyName { get; set; }
        public string BuildingID { get; set; }
        public string BuildingName { get; set; }
        public string FloorID { get; set; }
        public string FloorNo { get; set; }
        public string SuiteID { get; set; }
        public string SuiteNo { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string System { get; set; }
        public string EquipmentID { get; set; }
        public string EquipmentName { get; set; }
        public string Serial { get; set; }
        public string WarrantyExp { get; set; }
        public string AssignID { get; set; }
        public string AssignName { get; set; }
        public string FromID { get; set; }
        public string FromName { get; set; }
        public string PersonToNotifyID { get; set; }
        public string PersonToNotify { get; set; }
        public string EstimatedTime { get; set; }
        public string CompletedTime { get; set; }
        public string LocationTypeID { get; set; }
        public string LocationType { get; set; }
        public string LocationRequired { get; set; }
        public string UniqID { get; set; }
        public string DispatchDate { get; set; }
        public string DispatchTime { get; set; }
        public string Deficiency { get; set; }
        public string Priority { get; set; }
        public string Frequency { get; set; }
        public string Comments { get; set; }
        public string Attachments { get; set; }
        public string ScheduleType { get; set; }
        public string ScheduleStatus { get; set; }
        public string StartTime { get; set; }
        public string IsRecurring { get; set; }
        public string IsCorrective { get; set; }
        public string DescriptiveLocation { get; set; }
        public string LockStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime InsertedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
