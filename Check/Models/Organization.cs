using Check.Models;
using System;
using System.Collections.Generic;

namespace Check.Models
{
    public class Organization
    {
        public Organization()
        {
            People =new HashSet<Person>();
            Equipments = new HashSet<Equipment>();
            AbnormalReasons = new HashSet<AbnormalReason>();
        }
        
        public Guid OrganizationId { get; set; }
        public Guid ParentId { get; set; }
        public string OId { get; set; }
        public string Name { get; set; }       

        public virtual ICollection<Person> People { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }

        public virtual ICollection<AbnormalReason> AbnormalReasons { get; set; }
    }
}
