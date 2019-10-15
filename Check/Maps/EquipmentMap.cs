using Check.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Check.Maps
{
    public class EquipmentMap : EntityTypeConfiguration<Equipment>
    {
        public EquipmentMap()
        {
            HasRequired(e => e.Person)
                .WithMany(o => o.Equipments)
                .HasForeignKey(e => e.PersonId).WillCascadeOnDelete(false);

            Property(e => e.EquipmentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
