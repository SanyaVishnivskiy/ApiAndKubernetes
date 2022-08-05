namespace RestApi.Application.Categories.Commands.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .MaximumLength(Category.IdMaxLength);
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Category.NameMaxLength);
    }
}
