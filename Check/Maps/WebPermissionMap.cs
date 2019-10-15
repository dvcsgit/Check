using Check.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Check.Maps
{
    public class WebPermissionMap: EntityTypeConfiguration<WebPermission>
    {
        public WebPermissionMap()
        {
            //Property(w=>w.WebPermissionId)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //HasMany(p => p.WebFunctions)
            //    .WithMany(f => f.WebPermissions)
            //    .Map(m =>
            //    {
            //        m.ToTable("WebPermissionWebFunctions");                    
            //        m.MapLeftKey("WebPermissionId");
            //        m.MapRightKey("WebFunctionId");
            //    });
        }
    }
}
