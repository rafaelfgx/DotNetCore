namespace DotNetCore.MongoDB.Tests
{
    public sealed class FakeRepository : MongoRepository<FakeDocument>, IFakeRepository
    {
        public FakeRepository(FakeContext context) : base(context)
        {
        }
    }
}
