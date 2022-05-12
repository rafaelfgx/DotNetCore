using FluentValidation;

namespace DotNetCore.Mediator.Tests;

public class InsertCategoryCommandValidator : AbstractValidator<InsertCategoryCommand>
{
    public InsertCategoryCommandValidator()
    {
        RuleFor(category => category.Name).NotEmpty();
    }
}
