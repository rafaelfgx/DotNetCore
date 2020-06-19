using System.Collections.Generic;

namespace DotNetCore.EntityFrameworkCore.Tests
{
    public class FakeEntityModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public FakeValueObjectModel FakeValueObject { get; set; }

        public ICollection<FakeEntityChild> FakeEntityChild { get; set; } = new HashSet<FakeEntityChild>();
    }
}
