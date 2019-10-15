using Check.Maps;
using Check.Models;
using System.Data.Entity;

namespace Check
{
    public class CheckContext:DbContext
    {
        public CheckContext() : base("name=Check") { }

        public DbSet<Person> People { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<WebPermission> WebPermissions { get; set; }
        public DbSet<WebPermissionName> WebPermissionNames { get; set; }
        public DbSet<WebFunction> WebFunctions { get; set; }
        public DbSet<WebFunctionName> WebFunctionNames { get; set; }
        public DbSet<RolePermissionFunction> RolePermissionFunctions { get; set; }
        public DbSet<QueryableOrganization> QueryableOrganizations { get; set; }
        public DbSet<EditableOrganization> EditableOrganizations { get; set; }
        public DbSet<OrganizationManager> OrganizationManagers { get; set; }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<AbnormalReason> AbnormalReasons { get; set; }

        public DbSet<UploadLog> UploadLogs { get; set; }
        public DbSet<DeviceStatus> DeviceStatuses { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new OrganizationMap());
            modelBuilder.Configurations.Add(new WebPermissionMap());
            modelBuilder.Configurations.Add(new WebPermissionNameMap());
            modelBuilder.Configurations.Add(new WebFunctionNameMap());
            modelBuilder.Configurations.Add(new OrganizationManagerMap());
            modelBuilder.Configurations.Add(new RolePermissionFunctionMap());

            modelBuilder.Configurations.Add(new EquipmentMap());
            modelBuilder.Configurations.Add(new AbnormalReasonMap());
        }
    }
}
