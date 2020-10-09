using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AiTech.TrackableEntity
{
    [Serializable]
    public class EntityObjectCollection<TEntity>
        where TEntity : EntityObject
    {
        protected internal ICollection<TEntity> ItemCollection;

        public event EventHandler<EntityCollectionEventHandler> CollectionChanged;

        public IEnumerable<TEntity> Items { get; set; }

        public bool IsDirty { get; set; }


        public EntityObjectCollection()
        {
            ItemCollection = new List<TEntity>();
            Items          = ItemCollection.Where(o => o.StateStatus != EntityObjectState.Deleted);
        }


        public virtual void Add(TEntity item)
        {
            var itemFound = ItemCollection.FirstOrDefault(x => x.RowId == item.RowId);
            if (itemFound != null) throw new Exception("Record Already Exists");

            item.StateStatus = EntityObjectState.Created;
            item.IsNewRecord = true;

            item.PropertyChanged += (s, e) => SetDirty();
            ItemCollection.Add(item);
            SetDirty();

            OnCollectionChanged(new EntityCollectionEventHandler(CollectionEvent.Added, item));
        }

        protected bool SetDirty() => IsDirty = true;

        public virtual void AddRange(IEnumerable<TEntity> items)
        {
            foreach (var item in items)
                Add(item);
        }

        public virtual void Attach(TEntity item)
        {
            item.PropertyChanged += (s, e) => SetDirty();
            ItemCollection.Add(item);
            SetDirty();
            OnCollectionChanged(new EntityCollectionEventHandler(CollectionEvent.Attached, item));
        }

        public virtual void AttachRange(IEnumerable<TEntity> items)
        {
            foreach (var item in items)
                Attach(item);
        }

        public virtual void Remove(TEntity item)
        {
            if (item.StateStatus == EntityObjectState.Deleted) return;

            //Check if it has an Id. If it has, it means, it is from DB.
            if (item.Id == 0)
            {
                item.StateStatus = EntityObjectState.Deleted;
                ItemCollection.Remove(item);
            }
            else
            {
                // Find the User
                var foundItem = ItemCollection.FirstOrDefault(o => o.Id == item.Id || o.RowId == item.RowId);
                if (foundItem == null)
                    throw new Exception("Record Not Found");
                foundItem.StateStatus = EntityObjectState.Deleted;
            }

            SetDirty();
            OnCollectionChanged(new EntityCollectionEventHandler(CollectionEvent.Removed, item));
        }

        /// <summary>
        /// Remove All, Mark as Deleted, Tracks Changes
        /// </summary>
        public virtual void RemoveAll()
        {
            for (var i = ItemCollection.Count - 1; i >= 0; i--)
            {
                var item = ItemCollection.ElementAt(i);
                if (item.Id == 0)
                    ItemCollection.Remove(item);
                else
                    item.StateStatus = EntityObjectState.Deleted;

                OnCollectionChanged(new EntityCollectionEventHandler(CollectionEvent.Removed, item));
            }

            SetDirty();
        }


        /// <summary>
        /// Clears All Collection WITHOUT Tracking
        /// </summary>
        public virtual void ClearWithNoTracking()
        {
            ItemCollection.Clear();
            SetDirty();
        }


        public void RemoveDeletedItemsAndClearChanges()
        {
            var deletedItems = ItemCollection.Where(o => o.StateStatus == EntityObjectState.Deleted).ToList();

            foreach (var item in deletedItems)
                ItemCollection.Remove(item);

            foreach (var item in ItemCollection)
                item.StartTrackingChanges();

            StartTrackingChanges();
        }

        public void ResetIdOfNewRecords()
        {
            foreach (var item in ItemCollection)
            {
                if (!item.IsNewRecord)
                    continue;

                item.Id          = 0;
                item.StateStatus = EntityObjectState.Created;
            }
        }


        public void LoadItemsWith(IEnumerable<TEntity> items)
        {
            ItemCollection.Clear();
            foreach (var item in items)
            {
                item.StateStatus = EntityObjectState.NoChanges;
                item.StartTrackingChanges();
                ItemCollection.Add(item);
                OnCollectionChanged(new EntityCollectionEventHandler(CollectionEvent.Added, item));
            }
        }


        public IEnumerable<TEntity> GetDirtyItems()
        {
            return ItemCollection.Where(r => r.StateStatus != EntityObjectState.NoChanges);
        }

        public IEnumerable<TEntity> GetAllItems()
        {
            return ItemCollection;
        }


        public void StartTrackingChanges()
        {
            IsDirty = false;
        }


        protected void SetRelationshipKeyTo(INotifyPropertyChanged parentObject, Expression<Func<EntityObject, long>> parentProperty, Expression<Func<TEntity, long>> propertyToRelate)
        {
            var parentPropertyInfo = (PropertyInfo) ((MemberExpression) parentProperty.Body).Member;
            var itemPropertyInfo   = (PropertyInfo) ((MemberExpression) propertyToRelate.Body).Member;

            parentObject.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName != parentPropertyInfo.Name)
                    return;

                var parentPropertyValue = parentPropertyInfo.GetValue(parentObject, null);

                foreach (var item in ItemCollection)
                    itemPropertyInfo.SetValue(item, parentPropertyValue, null);
            };

            CollectionChanged += (s, e) =>
            {
                if (e.EventType == CollectionEvent.Removed) 
                    return;

                var parentPropertyValue = parentPropertyInfo.GetValue(parentObject, null);
                itemPropertyInfo.SetValue(e.Item, parentPropertyValue, null);
            };
        }

        protected virtual void OnCollectionChanged(EntityCollectionEventHandler e)
        {
            CollectionChanged?.Invoke(this, e);
        }
    }
}