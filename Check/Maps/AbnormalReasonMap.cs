using Check.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Check.Maps
{
    public class AbnormalReasonMap : EntityTypeConfiguration<AbnormalReason>
    {
        public AbnormalReasonMap()
        {
            Property(e => e.AbnormalReasonId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(x => x.Organization).WithMany(x => x.AbnormalReasons).HasForeignKey(x => x.OrganizationId).WillCascadeOnDelete(false);
        }
    }
}
