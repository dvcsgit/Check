using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check.Models
{
    public class Equipment
    {
        public Equipment()
        {

        }

        public Guid EquipmentId { get; set; }
        public string EId { get; set; }

        public string Position { get; set; }

        public string Type { get; set; }

        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }        

        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }

        public string Enable { get; set; }

        public DateTime LastModifyTime { get; set; }
    }
}
