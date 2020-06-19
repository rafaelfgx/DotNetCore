using FluentValidation;

namespace DotNetCore.Mediator.Tests
{
    public class InsertCategoryCommandValidator : AbstractValidator<InsertCategoryCommand>
    {
        public InsertCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
