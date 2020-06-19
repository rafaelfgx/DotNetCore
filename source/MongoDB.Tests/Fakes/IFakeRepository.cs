using DotNetCore.Repositories;

namespace DotNetCore.MongoDB.Tests
{
    public interface IFakeRepository : IRepository<FakeDocument> { }
}
