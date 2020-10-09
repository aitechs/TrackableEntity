using AiTech.TrackableEntity;

namespace TrackableEntity.Test.TestObject
{
    public class ChildCollection : EntityObjectCollection<Child>
    {
        public ChildCollection(Employee parentEmployee)
        {
            SetRelationshipKeyTo(parentEmployee, _ => _.Id, _ => _.EmployeeId);
        }
    }
}