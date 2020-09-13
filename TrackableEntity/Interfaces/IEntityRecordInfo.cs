using System;

namespace AiTech.TrackableEntity
{
    public interface IEntityRecordInfo
    {
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}