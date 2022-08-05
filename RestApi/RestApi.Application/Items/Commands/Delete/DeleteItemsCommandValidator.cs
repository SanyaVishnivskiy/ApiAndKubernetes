namespace RestApi.Application.Items.Commands.Delete;

public class DeleteItemsCommandValidator : AbstractValidator<DeleteItemCommand>
{
    public DeleteItemsCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .MaximumLength(Category.IdMaxLength);
    }
}
