using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using System;

namespace DotNetCore.MongoDB.Tests
{
    [TestClass]
    public class MongoTests
    {
        private readonly IFakeRepository _repository;

        public MongoTests()
        {
            var services = new ServiceCollection();

            const string connectionString = "mongodb://localhost/Database";

            services.AddScoped(x => new FakeContext(connectionString));

            var context = services.BuildServiceProvider().GetService<FakeContext>();

            _repository = new FakeRepository(context);
        }

        private static FakeDocument Document => new FakeDocument
        {
            Id = ObjectId.GenerateNewId(),
            Name = Guid.NewGuid().ToString()
        };

        [TestMethod]
        public void Tests()
        {
            var document = Document;

            _repository.Add(document);

            document = _repository.Get(document.Id);

            Assert.IsNotNull(document);
        }
    }
}
