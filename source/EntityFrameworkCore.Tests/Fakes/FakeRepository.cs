namespace DotNetCore.EntityFrameworkCore.Tests
{
    public sealed class FakeRepository : EFRepository<FakeEntity>, IFakeRepository
    {
        public FakeRepository(FakeContext context) : base(context)
        {
        }
    }
}
