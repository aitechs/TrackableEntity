using AiTech.Trackable;
using System;

namespace AiTech.TrackableEntity
{
    public abstract class SimpleEntityObject : TrackableObject, IEntityObject
    {
        protected SimpleEntityObject()
        {
            RowId       = Guid.NewGuid();
            StateStatus = EntityObjectState.Created;
        }


        private long _id;

        public long Id
        {
            get => _id;
            set => SetPropertyValue(ref _id, nameof(Id), value);
        }

        public Guid RowId { get; set; }


        public bool IsNewRecord { get; set; }

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

      

        public override void StartTrackingChanges()
        {
            StateStatus = EntityObjectState.NoChanges;
            base.StartTrackingChanges();
        }

        protected override void OnPropertyChanged(object sender, TrackablePropertyChangedEventObject arg)
        {
            base.OnPropertyChanged(sender, arg);
            if(StateStatus == EntityObjectState.NoChanges)
                StateStatus = EntityObjectState.Modified;
        }
    }
}