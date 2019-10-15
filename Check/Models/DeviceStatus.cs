using System;

namespace Check.Models
{
    public class DeviceStatus
    {
        public Guid DeviceStatusId { get; set; }
        public string AppVersion { get; set; }
        public string IMEI { get; set; }
        public string MacAddress { get; set; }
        public DateTime? LastUpdateTime { get; set; }
    }
}
