using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AlertModel
    {
        public string Type { get; set; }
        public string SensorId { get; set; }
        public string Time { get; set; }
        public string Value { get; set; }
        public string BuildingID { get; set; }
        public string FloorID { get; set; }
        public string DisplayName { get; set; }
        public string SensorType { get; set; }
        public string Command { get; set; }
        public string CommandInitiator { get; set; }
        public string CommandStatusText { get; set; }
        public string Severity { get; set; }
        public string Resolved { get; set; }
        public string TrackingNumber { get; set; }
        public string FloorNo { get; set; }
        public string ZoneName { get; set; }
        public string VerificationCode { get; set; }
        //public string VerificationCodeValidity { get; set; }
        public string AlertTitle { get; set; }
        public string ResolvedById { get; set; }
        public string ResolvedByName { get; set; }
        public string ResolvedTime { get; set; }
        public string VerificationCodeNeeded { get; set; }
        public string PTrackingNumber { get; set; }
        public string AutoShutOff { get; set; }
        public string ActionRequired { get; set; }
        public string ActionTime { get; set; }
        public string NoOfAlert { get; set; }
        public string SuiteId { get; set; }
        public string SuiteNo { get; set; }
        public string SourceDeviceId { get; set; }
        public string ActionCompleted { get; set; }
        public string AlertPosition { get; set; }
        public string ViewHistory { get; set; }
        public string ServiceLayer { get; set; }
        public string AutoAcknowledge { get; set; }
        public string AutoAcknowledgeHour { get; set; }
        public string DeviceType { get; set; }
        public string SourceDeviceType { get; set; }
        public DateTime? InsertedDate { get; set; }
    }
}
