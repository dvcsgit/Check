using Check.Models;
using System.Data.Entity.ModelConfiguration;

namespace Check.Maps
{
    public class WebFunctionNameMap : EntityTypeConfiguration<WebFunctionName>
    {
        public WebFunctionNameMap()
        {
            HasKey(wfn => new { wfn.WebFunctionId, wfn.Language });
        }
    }
}
