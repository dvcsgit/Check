using Check.Models;
using System.Data.Entity.ModelConfiguration;

namespace Check.Maps
{
    public class OrganizationManagerMap:EntityTypeConfiguration<OrganizationManager>
    {
        public OrganizationManagerMap()
        {
            HasKey(om =>new { om.OrganizationId,om.ManagerId });
            //One to one relationship.Set the field "OrganizationId" to be the pirmary key and foreign key.
            //HasRequired(om => om.ManagerOf).WithOptional(o => o.Manager);
        }
    }
}
