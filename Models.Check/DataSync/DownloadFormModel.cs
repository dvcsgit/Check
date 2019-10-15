using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Check.DataSync
{
    public class DownloadFormModel
    {
        public string CheckDate { get; set; }
        public List<DownloadParameter> DownloadParameters { get; set; }
        public DownloadFormModel()
        {
            DownloadParameters = new List<DownloadParameter>();
        }
    }
}
