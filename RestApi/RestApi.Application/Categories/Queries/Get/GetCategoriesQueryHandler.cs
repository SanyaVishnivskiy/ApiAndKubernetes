namespace RestApi.Application.Categories.Queries.Get;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<Category>>
{
    private readonly ICategoriesRepository _repository;

    public GetCategoriesQueryHandler(ICategoriesRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Get(cancellationToken);
    }
}
