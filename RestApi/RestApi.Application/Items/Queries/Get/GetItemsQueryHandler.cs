namespace RestApi.Application.Items.Queries.Get;

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, List<Item>>
{
    private readonly IItemsRepository _repository;

    public GetItemsQueryHandler(IItemsRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Item>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        var options = new ItemsFilterOptions
        {
            CategoryId = request.CategoryId,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
        };

        return await _repository.Get(options, cancellationToken);
    }
}
