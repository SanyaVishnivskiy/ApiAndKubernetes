namespace RestApi.Application.Items.Commands.Update;

public class UpdateItemsCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemsCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .MaximumLength(Category.IdMaxLength);
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Category.NameMaxLength);
    }
}
