using System;

namespace Models.Check.Equipment
{
    public class EquipmentModel
    {
        public string EquipmentId { get; set; }
        public string EId { get; set; }
        public string Position { get; set; }
        public string Type { get; set; }
        public string Department { get; set; }        
        public string Creator { get; set; }
        public string Enable { get; set; }
        public DateTime LastModifyTime { get; set; }
    }
}
