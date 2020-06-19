using System.Collections.Generic;
using DotNetCore.Domain;

namespace DotNetCore.EntityFrameworkCore.Tests
{
    public class FakeEntity : Entity<long>
    {
        public FakeEntity() : base(0)
        {
        }

        public FakeEntity(string name) : base(0)
        {
            Name = name;
        }

        public FakeEntity(string name, string surname, FakeValueObject fakeValueObject) : base(0)
        {
            Name = name;
            Surname = surname;
            FakeValueObject = fakeValueObject;
            FakeEntityChild = new List<FakeEntityChild> { new FakeEntityChild(0) { FakeEntityId = Id } };
        }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public FakeValueObject FakeValueObject { get; private set; }

        public IReadOnlyCollection<FakeEntityChild> FakeEntityChild { get; private set; }
    }
}
