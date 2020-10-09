using System.ComponentModel;

namespace AiTech.TrackableEntity
{
    public class EntityCollectionEventHandler : HandledEventArgs
    {
        public CollectionEvent EventType { get; }

        public EntityObject Item { get; }

        public EntityCollectionEventHandler(CollectionEvent eventType, EntityObject item)
        {
            EventType = eventType;
            Item      = item;
        }
    }
}