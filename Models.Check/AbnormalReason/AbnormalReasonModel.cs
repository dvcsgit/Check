using System;

namespace Models.Check.AbnormalReason
{
    public class AbnormalReasonModel
    {
        public string AbnormalReasonId { get; set; }
        public string ARId { get; set; }
        public string Reason { get; set; }
        public string Dept { get; set; }
        public string Creator { get; set; }
        public string Enable { get; set; }
        public DateTime LastModifyTime { get; set; }
    }
}
