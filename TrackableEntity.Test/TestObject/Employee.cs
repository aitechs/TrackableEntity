using AiTech.TrackableEntity;
using System;
using AiTech.Trackable;

namespace TrackableEntity.Test.TestObject
{
    public class Employee : EntityObject
    {
        public Employee()
        {
            ChildrenWithRelationship = new ChildCollection(this);
            Children                 = new Child2Collection(this);
            
            AddressInfo = new PersonAddress();
            RaisePropertyChangedOnChangesIn(AddressInfo, nameof(AddressInfo));
            //AddressInfo.PropertyChanged += (s, e) =>
            //{
            //    OnPropertyChanged(s, (TrackablePropertyChangedEventObject) e);
            //};

        }

        public PersonAddress AddressInfo { get; set; }


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




        public ChildCollection ChildrenWithRelationship { get; }
        public Child2Collection Children { get; }

      
    }
}
