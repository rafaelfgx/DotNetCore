using DotNetCore.Repositories;

namespace DotNetCore.EntityFrameworkCore.Tests
{
    public sealed class FakeSpecification : Specification<FakeEntity>
    {
        public FakeSpecification()
        {
            ApplyWhere(entity => entity.Id > 0);
            ApplyOrderByDescending(entity => entity.Id);
            ApplySkipTake(0, 5);
            AddInclude(entity => entity.FakeEntityChild);
        }
    }
}
