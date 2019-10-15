using System.Collections.Generic;

namespace Models.Check.DataSync
{
    public class DownloadDataModel
    {
        public DownloadDataModel()
        {
            PersonModels = new List<PersonModel>();
            EquipmentModels = new List<EquipmentModel>();
        }

        public List<PersonModel> PersonModels { get; set; }

        public List<EquipmentModel> EquipmentModels { get; set; }

        public List<AbnormalReasonModel> AbnormalReasonModels { get; set; }
    }
}
