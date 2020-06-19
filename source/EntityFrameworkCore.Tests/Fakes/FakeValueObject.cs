using System.Collections.Generic;
using DotNetCore.Domain;

namespace DotNetCore.EntityFrameworkCore.Tests
{
    public class FakeValueObject : ValueObject
    {
        public FakeValueObject()
        {
        }

        public FakeValueObject(string property1, string property2)
        {
            Property1 = property1;
            Property2 = property2;
        }

        public string Property1 { get; private set; }

        public string Property2 { get; private set; }

        protected override IEnumerable<object> GetEquals()
        {
            yield return Property1;
            yield return Property2;
        }
    }
}
