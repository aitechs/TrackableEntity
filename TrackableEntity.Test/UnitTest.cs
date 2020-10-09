using AiTech.TrackableEntity;
using System;
using System.Diagnostics;
using System.Linq;
using TrackableEntity.Test.TestObject;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TrackableEntity.Test
{

    public class UnitTest
    {
        private static Employee DefaultEmployee()
        {
            return new Employee()
            {
                LastName  = "Rizal",
                FirstName = "Jose",
                BirthDate = new DateTime(1861, 6, 19)
            };
        }

        private static Employee CreateEmployee()
        {
            return new Employee()
            {
                LastName       = Faker.Name.Last(),
                FirstName      = Faker.Name.First(),
                BirthDate      = Faker.Identification.DateOfBirth(),
                EmployeeNumber = Faker.RandomNumber.Next(0, 200)
            };
        }


        [Fact]
        public void EmployeeDirtyTest()
        {
            var p = CreateEmployee();

            Assert.IsTrue(p.IsDirty, "Object Must Be Dirty");

            p.StartTrackingChanges();
            Assert.IsFalse(p.IsDirty, "Object Must NOT be dirty");
        }


        [Fact]
        [Trait("Category","Collection")]
        public void CollectionCreateNewTest()
        {
            var col = new EmployeeCollection();
            Assert.IsFalse(col.IsDirty, "Collection is dirty");
        }

        [Fact]
        [Trait("Category","Collection")]
        public void CollectionAddItemTest()
        {
            var defaultCount = 4;

            var col = new EmployeeCollection();
            for (var i = 1; i <= defaultCount; i++)
                col.Add(CreateEmployee());

            Assert.IsTrue(col.IsDirty, "Collection Must Be Dirty");
            Assert.AreEqual(defaultCount, col.Items.Count(), "Total Items NOT Equal");
        }

        [Fact]
        [Trait("Category","Collection")]
        public void CollectionResetDirtyTest()
        {
            var col = new EmployeeCollection();
            for (var i = 0; i < 2; i++)
                col.Add(CreateEmployee());

            col.StartTrackingChanges();
            Assert.IsFalse(col.IsDirty, "Collection Must NOT BE Dirty");
        }


        [Fact]
        [Trait("Category","Collection")]
        public void CollectionAttachItemTest()
        {
            var col = new EmployeeCollection();
            for (var i = 0; i < 2; i++)
                col.Add(CreateEmployee());

            Assert.IsTrue(col.IsDirty, "Collection Must Be Dirty");
            col.IsDirty = false;
        }


        [Fact]
        [Trait("Category","Collection")]
        public void CollectionRemoveItemTest()
        {
            var col = new EmployeeCollection();
            for (var i = 1; i <= 5; i++)
                col.Add(CreateEmployee());

            col.StartTrackingChanges();
            var p = col.Items.First();

            col.Remove(p);

            Assert.IsTrue(col.IsDirty);
            Assert.AreEqual(4, col.Items.Count());
            Assert.AreEqual(EntityObjectState.Deleted, p.StateStatus  );
        }

        
        [Fact]
        [Trait("Category","Collection")]
        public void CollectionCommitTest()
        {
            var col = new EmployeeCollection();
            for (var i = 1; i <= 5; i++)
            {
                var e = CreateEmployee();
                e.EmployeeNumber = i;
                col.Attach(e);
            }

            col.StartTrackingChanges();
            var p = col.Items.First(_=>_.EmployeeNumber == Faker.RandomNumber.Next(2,4));
            
            Debug.WriteLine($"Employee No: {p.EmployeeNumber}");
            
            p.StateStatus = EntityObjectState.Deleted;
            
            Assert.IsTrue(p.IsDirty, "Object Must be Dirty");
            //Assert.IsTrue(col.IsDirty, "Collection must be Dirty");
            col.RemoveDeletedItemsAndClearChanges();

            Assert.IsFalse(col.IsDirty, "Collection Must NOT be dirty");
            Assert.AreEqual(4, col.Items.Count());
        }


        [Fact]
        [Trait("Category","Collection")]
        public void CollectionEditItemTest()
        {
            var col = new EmployeeCollection();
            for (var i = 1; i <= 5; i++)
            {
                var emp = CreateEmployee();
                emp.StartTrackingChanges();
                col.Attach(emp);
            }


            col.StartTrackingChanges();
            var p = col.Items.First();

            p.FirstName = "ChangedName";

            Debug.WriteLine($"Emp No. => {p.EmployeeNumber}");
            Assert.IsTrue(col.IsDirty,"Collection Must Be Dirty");
            Assert.IsTrue(p.IsDirty, "Item Must be dirty");
            
            Assert.AreEqual(EntityObjectState.Modified, p.StateStatus,"StateStatus NOT Same");
        }
    }
}