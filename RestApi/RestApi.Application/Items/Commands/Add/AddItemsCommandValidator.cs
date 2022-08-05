namespace RestApi.Application.Items.Commands.Add;

public class AddItemsCommandValidator : AbstractValidator<AddItemCommand>
{
    public AddItemsCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Item.NameMaxLength);
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .MaximumLength(Item.CategoryIdMaxLength);
    }
}
