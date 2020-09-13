using AiTech.Trackable;
using System;

namespace AiTech.TrackableEntity
{
    public abstract class EntityObject : TrackableObject
    {
        public long Id { get; set; }

        public Guid RowId { get; set; }


        public bool IsNewRecord { get; set; }

        public RecordInfo RecordInfo { get; }

        public byte RowVersion { get; set; }


        private EntityObjectState _stateStatus;

        public EntityObjectState StateStatus
        {
            get => _stateStatus;
            set
            {
                _stateStatus = value;
                IsDirty      = true;
            }
        }

        protected EntityObject()
        {
            RowId       = Guid.NewGuid();
            StateStatus = EntityObjectState.Created;
            RecordInfo  = new RecordInfo();
        }


        public override void StartTrackingChanges()
        {
            StateStatus = EntityObjectState.NoChanges;
            base.StartTrackingChanges();
        }

        protected override void OnPropertyChanged(object sender, TrackablePropertyChangedEventObject arg)
        {
            base.OnPropertyChanged(sender, arg);
            StateStatus = EntityObjectState.Modified;
        }
    }
}