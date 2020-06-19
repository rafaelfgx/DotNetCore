namespace DotNetCore.Mediator.Tests
{
    public class CategoryByIdQuery : IRequest
    {
        public long Id { get; set; }
    }
}
