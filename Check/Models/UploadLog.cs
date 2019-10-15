using System;

namespace Check.Models
{
    public class UploadLog
    {
        public Guid UploadLogId { get; set; }
        public string UploadPId { get; set; }
        public DateTime UploadTime { get; set; }
        public DateTime? TransTime { get; set; }
        public string Message { get; set; }
    }
}
