using System;
using System.ComponentModel;

namespace AiTech.TrackableEntity
{
    public interface IEntityObject : INotifyPropertyChanged
    {
        long Id { get; set; }
        Guid RowId { get; set; }
        EntityObjectState StateStatus { get; set; }

        bool IsNewRecord { get; set; }

        void StartTrackingChanges();
    }
}