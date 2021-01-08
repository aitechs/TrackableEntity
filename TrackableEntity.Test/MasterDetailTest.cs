using TrackableEntity.Test.TestObject;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TrackableEntity.Test
{
    public class MasterDetailTest
    {
        [Fact]
        public void OnNoRelationshipNoError_WhenAddedOnList()
        {
            var master = new Employee();
            var child  = new Child() {Name = "ChildName", EmployeeId = 1};

            master.Id = 2;
            master.Children.Add(child);
            Assert.AreNotEqual(master.Id, child.EmployeeId);
        }


        [Fact]
        public void OnRelationship_UpdateLinkedItemPropertyValue_WhenMasterIdChanged()
        {
            var master = new Employee() {Id = 1};
            var child  = new Child() {Name = "ChildName"};

            master.ChildrenWithRelationship.Add(child);

            Assert.AreEqual(1, master.Id);
            Assert.AreEqual(1, child.EmployeeId);

            //Change the Master
            master.Id = 100;

            //Expect the Foreign Key to Change
            Assert.AreEqual(100, child.EmployeeId);
        }


        [Fact]
        public void OnRelationship_UpdateLinkedItemPropertyValue_WhenAddedOnList()
        {
            var master = new Employee();
            var child  = new Child() {Name = "ChildName", EmployeeId = 1};
            var child2 = new Child() {Name = "ChildName", EmployeeId = 1};

            master.Id = 2;
            master.ChildrenWithRelationship.Add(child);
            Assert.AreEqual(2, child.EmployeeId);

            master.ChildrenWithRelationship.Attach(child2);
            Assert.AreEqual(2, child.EmployeeId);

            master.Id = 3;
            Assert.AreEqual(3, child.EmployeeId);
            Assert.AreEqual(3, child2.EmployeeId);
        }
    }
}