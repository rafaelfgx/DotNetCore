namespace DotNetCore.Mediator.Tests
{
    public class InsertCategoryCommand : IRequest
    {
        public string Name { get; set; }
    }
}
