using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotNetCore.Mediator.Tests
{
    [TestClass]
    public class MediatorTests
    {
        private readonly IMediator _mediator;

        public MediatorTests()
        {
            var services = new ServiceCollection();

            services.AddMediator(typeof(MediatorTests).Assembly);

            _mediator = services.BuildServiceProvider().GetRequiredService<IMediator>();
        }

        [TestMethod]
        public void CategoryByIdQuery()
        {
            var query = new CategoryByIdQuery { Id = 1L };

            var response = _mediator.HandleAsync<CategoryByIdQuery, Category>(query).Result;

            Assert.IsTrue(response.Succeeded);
            Assert.IsNotNull(response.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void DeleteCategoryCommand_ShouldFail()
        {
            var command = new DeleteCategoryCommand { Id = 1 };

            _mediator.HandleAsync(command).Wait();
        }

        [TestMethod]
        public void InsertCategoryCommand()
        {
            var command = new InsertCategoryCommand { Name = "Name" };

            var response = _mediator.HandleAsync<InsertCategoryCommand, long>(command).Result;

            Assert.IsTrue(response.Succeeded);
            Assert.IsTrue(response.Data > 0);
        }

        [TestMethod]
        public void InsertCategoryCommand_ShouldFail()
        {
            var command = new InsertCategoryCommand();

            var response = _mediator.HandleAsync<InsertCategoryCommand, long>(command).Result;

            Assert.IsTrue(response.Failed);
        }

        [TestMethod]
        public void UpdateCategoryCommand()
        {
            var command = new UpdateCategoryCommand { Id = 1, Name = "Name Updated" };

            var response = _mediator.HandleAsync(command).Result;

            Assert.IsTrue(response.Succeeded);
        }
    }
}
