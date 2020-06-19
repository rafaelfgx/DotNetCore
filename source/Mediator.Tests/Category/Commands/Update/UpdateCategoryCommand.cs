namespace DotNetCore.Mediator.Tests
{
    public class UpdateCategoryCommand : IRequest
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
