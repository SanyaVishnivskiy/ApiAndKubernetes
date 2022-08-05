namespace RestApi.Application.Items.Queries.Get;

public class GetItemsQueryValidator : AbstractValidator<GetItemsQuery>
{
    public GetItemsQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty();
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .When(x => x.PageNumber is not null);
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .When(x => x.PageSize is not null);
    }
}
