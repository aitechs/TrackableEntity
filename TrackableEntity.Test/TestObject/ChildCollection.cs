using AiTech.TrackableEntity;

namespace TrackableEntity.Test.TestObject
{
    public class ChildCollection : EntityObjectCollection<Child>
    {
        private readonly Employee _parentEmployee;

        public ChildCollection(Employee parentEmployee)
        {
            _parentEmployee = parentEmployee;
            SetRelationshipKeyTo(_parentEmployee, _ => _.Id, _ => _.EmployeeId);
        }
    }
}