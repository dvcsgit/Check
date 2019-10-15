using System;

namespace Check.Models
{
    public class AbnormalReason
    {
        public Guid AbnormalReasonId { get; set; }

        public string ARId { get; set; }

        public string Reason { get; set; }

        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }

        public string Enable { get; set; }

        public DateTime LastModifyTime { get; set; }
    }
}
