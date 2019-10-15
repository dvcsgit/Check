using Check.Models;
using System;
using System.Collections.Generic;

namespace Check.Models
{
    public class Person
    {
        public Person()
        {
            Roles = new HashSet<Role>();
            Equipments = new HashSet<Equipment>();
            //ManageOrganizations = new HashSet<Organization>();
        }
        public Guid PersonId { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Section { get; set; }        
        public bool IsMobilePerson { get; set; }
        public DateTime? LastModifyTime { get; set; }        

        public virtual ICollection<Role> Roles { get; set; }
        
        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }

        //public virtual ICollection<Organization> ManageOrganizations { get; set; }
    }
}
