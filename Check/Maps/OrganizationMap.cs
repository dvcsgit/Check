﻿using Check.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Check.Maps
{
    public class OrganizationMap: EntityTypeConfiguration<Organization>
    {
        public OrganizationMap()
        {
            //Property(o => o.OrganizationId)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //HasMany(o => o.People)
            //    .WithRequired(p => p.Organization)
            //    .HasForeignKey(p => p.OrganizationId);


        }
    }
}
