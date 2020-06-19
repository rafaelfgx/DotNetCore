namespace DotNetCore.MongoDB.Tests
{
    public sealed class FakeContext : MongoContext
    {
        public FakeContext(string connectionString) : base(connectionString)
        {
        }
    }
}
