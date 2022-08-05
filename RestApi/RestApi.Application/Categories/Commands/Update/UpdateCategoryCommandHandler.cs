namespace RestApi.Application.Categories.Commands.Update;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly ICategoriesRepository _repository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(ICategoriesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request);

        var udpated = await TryUpdate(category);
        if (udpated)
        {
            await _repository.SaveChanges();
        }

        return Unit.Value;
    }

    private async Task<bool> TryUpdate(Category category)
    {
        try
        {
            await _repository.Update(category);
            return true;
        }
        catch (EntityNotFoundException)
        {
            return false;
        }
    }
}
