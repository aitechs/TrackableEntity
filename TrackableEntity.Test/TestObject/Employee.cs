using AiTech.TrackableEntity;
using System;

namespace TrackableEntity.Test.TestObject
{
    public class Employee : EntityObject
    {
        
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetPropertyValue<string>(ref _lastName, nameof(LastName), value);
        }



        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetPropertyValue<string>(ref _firstName, nameof(FirstName), value);
        }




        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get => _birthDate;
            set => SetPropertyValue<DateTime>(ref _birthDate, nameof(BirthDate), value);
        }




        private int _employeeNumber;
        public int EmployeeNumber
        {
            get => _employeeNumber;
            set => SetPropertyValue<int>(ref _employeeNumber, nameof(EmployeeNumber), value);
        }




        public ChildCollection Children { get; }
        public Employee()
        {
            Children = new ChildCollection(this);
        }
    }
}
