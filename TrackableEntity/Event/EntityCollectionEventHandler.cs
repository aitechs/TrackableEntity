using System.ComponentModel;

namespace AiTech.TrackableEntity
{
    public class EntityCollectionEventHandler : HandledEventArgs
    {
        public CollectionEvent EventType { get; }

        public IEntityObject Item { get; }

        public EntityCollectionEventHandler(CollectionEvent eventType, IEntityObject item)
        {
            EventType = eventType;
            Item      = item;
        }
    }
}