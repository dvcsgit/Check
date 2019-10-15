using Check.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Check.Maps
{
    public class PersonMap:EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            Property(p=>p.PersonId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.LoginId)
                .HasMaxLength(32);
        }
    }
}
