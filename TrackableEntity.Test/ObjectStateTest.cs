using AiTech.TrackableEntity;
using TrackableEntity.Test.TestObject;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TrackableEntity.Test
{
    public class ObjectStateTest
    {
        [Fact]
        public void ObjectStatus_MustBeCreated_WhenNewObjectPropertyChanged()
        {
            var item = new Employee();

            Assert.AreEqual(EntityObjectState.Created, item.StateStatus);


            item.LastName = "Modified LastName";

            Assert.AreEqual(EntityObjectState.Created, item.StateStatus);

        }

        [Fact]
        public void ObjectStatus_MustBeModified_WhenPropertyChangedAfterStartTrackingChangedCall()
        {
            var item = new Employee();

            Assert.AreEqual(EntityObjectState.Created, item.StateStatus);
            item.LastName = "Initial LastName";
            item.StartTrackingChanges();
            item.LastName = "Modified LastName";

            Assert.AreEqual(EntityObjectState.Modified, item.StateStatus);
        }
    }
}
