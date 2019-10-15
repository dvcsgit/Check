using Check;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessor
{
    public class DeleteHelper
    {
        public static void DeleteOrganization(CheckContext context, List<Guid> organizationIds)
        {
            foreach (var organizationId in organizationIds)
            {
                context.Organizations.Remove(context.Organizations.First(o => o.OrganizationId == organizationId));
                //context.OrganizationManagers.RemoveRange(context.OrganizationManagers.Where(om => om.OrganizationId == organizationId));
                context.EditableOrganizations.RemoveRange(context.EditableOrganizations.Where(eo => eo.OrganizationId == organizationId));
                context.QueryableOrganizations.RemoveRange(context.QueryableOrganizations.Where(qo => qo.OrganizationId == organizationId));
            }

        }

        
    }
}
