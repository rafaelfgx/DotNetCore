using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCore.Mediator.Tests;

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
        var query = new CategoryByIdQuery(1);

        var response = _mediator.HandleAsync<CategoryByIdQuery, Category>(query).Result;

        Assert.IsTrue(response.Succeeded && response.Data != default);
    }

    [TestMethod]
    [ExpectedException(typeof(AggregateException))]
    public void DeleteCategoryCommandShouldThrowException()
    {
        var command = new DeleteCategoryCommand(1);

        _mediator.HandleAsync(command).Wait();
    }

    [TestMethod]
    public void InsertCategoryCommand()
    {
        var command = new InsertCategoryCommand("Name");

        var response = _mediator.HandleAsync<InsertCategoryCommand, long>(command).Result;

        Assert.IsTrue(response.Succeeded && response.Data > 0);
    }

    [TestMethod]
    public void InsertCategoryCommandShouldValidationFail()
    {
        var command = new InsertCategoryCommand(string.Empty);

        var response = _mediator.HandleAsync<InsertCategoryCommand, long>(command).Result;

        Assert.IsTrue(response.Failed);
    }

    [TestMethod]
    public void UpdateCategoryCommand()
    {
        var command = new UpdateCategoryCommand(1, "Name");

        var response = _mediator.HandleAsync(command).Result;

        Assert.IsTrue(response.Succeeded);
    }
}
