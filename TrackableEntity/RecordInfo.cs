using System;
using System.Data;

namespace AiTech.TrackableEntity
{
    public class RecordInfo : IEntityRecordInfo
    {
        public string CreatedBy  { get; set; }
        public string ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }


        public RecordInfo()
        {
            CreatedDate  = new DateTime(1920, 1, 1);
            ModifiedDate = new DateTime(1920, 1, 1);

            CreatedBy  = string.Empty;
            ModifiedBy = string.Empty;
        }


        public void LoadValuesFrom(IDataReader reader)
        {
            CreatedDate  = reader.GetDateTime(reader.GetOrdinal("Created"));
            CreatedBy    = reader.GetString(reader.GetOrdinal("CreatedBy"));
            ModifiedDate = reader.GetDateTime(reader.GetOrdinal("Modified"));
            ModifiedBy   = reader.GetString(reader.GetOrdinal("ModifiedBy"));
        }
    }
}
