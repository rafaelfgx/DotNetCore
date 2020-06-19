using DotNetCore.Domain;

namespace DotNetCore.EntityFrameworkCore.Tests
{
    public class FakeEntityChild : Entity<long>
    {
        public FakeEntityChild(long id) : base(id) { }

        public long FakeEntityId { get; set; }

        public FakeEntity FakeEntity { get; set; }
    }
}
