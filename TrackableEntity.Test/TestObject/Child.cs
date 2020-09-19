using AiTech.TrackableEntity;

namespace TrackableEntity.Test.TestObject
{
    public class Child : EntityObject
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => SetPropertyValue(ref _name, nameof(Name), value);
        }


        public long EmployeeId { get; set; }
    }
}