using AiTech.TrackableEntity;

namespace TrackableEntity.Test.TestObject
{
    public class Child2Collection : EntityObjectCollection<Child>
    {
        private readonly Employee _employee;

        public Child2Collection(Employee employee)
        {
            _employee = employee;
        }
    }
}