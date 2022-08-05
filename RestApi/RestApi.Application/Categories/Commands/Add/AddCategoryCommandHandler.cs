namespace RestApi.Application.Categories.Commands.Add;

public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, string>
{
    private readonly ICategoriesRepository _repository;
    private readonly IMapper _mapper;

    public AddCategoryCommandHandler(ICategoriesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<string> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request);
        var id = Guid.NewGuid().ToString();
        category.Id = id;

        await _repository.Add(category);
        await _repository.SaveChanges();

        return id;
    }
}
