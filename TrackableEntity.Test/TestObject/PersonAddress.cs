using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiTech.TrackableEntity;

namespace TrackableEntity.Test.TestObject
{
    public class PersonAddress :EntityObject
    {
        private string _province;

        public string Province
        {
            get => _province;
            set => SetPropertyValue(ref _province, nameof(Province), value);
        }

        private string _town;

        public string Town
        {
            get => _town;
            set => SetPropertyValue(ref _town, nameof(Town), value);
        }
    }
}
