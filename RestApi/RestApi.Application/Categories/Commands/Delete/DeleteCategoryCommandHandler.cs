namespace RestApi.Application.Categories.Commands.Delete;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ICategoriesRepository _repository;

    public DeleteCategoryCommandHandler(ICategoriesRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var deleted = await TryDelete(request.Id);
        if (deleted)
        {
            await _repository.SaveChanges();
        }

        return Unit.Value;
    }

    private async Task<bool> TryDelete(string id)
    {
        try
        {
            await _repository.Delete(id);
            return true;
        }
        catch (EntityNotFoundException)
        {
            return false;
        }
    }
}
