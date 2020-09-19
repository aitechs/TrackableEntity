using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrackableEntity.Test.TestObject;

namespace TrackableEntity.Test
{
    [TestClass]
    public class MasterDetailTest
    {
        [TestMethod]
        public void ChangeMasterIdTest()
        {
            var master = new Employee();

            var child = new Child() {Name = "ChildName"};

            master.Children.Add(child);

            Assert.AreEqual(0, master.Id);
            Assert.AreEqual(0, child.EmployeeId);

            //Change the Master
            master.Id = 100;

            //Expect the Foreign Key to Change
            Assert.AreEqual(100, child.EmployeeId);
        }
    }
}
