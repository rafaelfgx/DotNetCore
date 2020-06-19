namespace DotNetCore.Mediator.Tests
{
    public class DeleteCategoryCommand : IRequest
    {
        public long Id { get; set; }
    }
}
