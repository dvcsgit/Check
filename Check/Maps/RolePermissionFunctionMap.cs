using Check.Models;
using System.Data.Entity.ModelConfiguration;

namespace Check.Maps
{
    public class RolePermissionFunctionMap:EntityTypeConfiguration<RolePermissionFunction>
    {
        public RolePermissionFunctionMap()
        {           
            HasKey(wpwf=>new { wpwf.RoleId,wpwf.WebPermissionId,wpwf.WebFunctionId});            

            ToTable("Role_Permission_Functions");           
        }
    }
}
