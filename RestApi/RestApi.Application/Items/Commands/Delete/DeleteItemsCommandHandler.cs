namespace RestApi.Application.Items.Commands.Delete;

public class DeleteItemsCommandHandler : IRequestHandler<DeleteItemCommand>
{
    private readonly IItemsRepository _repository;

    public DeleteItemsCommandHandler(IItemsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var deleted = await TryDelete(request.Id, request.CategoryId);
        if (deleted)
        {
            await _repository.SaveChanges();
        }

        return Unit.Value;
    }

    private async Task<bool> TryDelete(string id, string categoryId)
    {
        try
        {
            await _repository.Delete(id, categoryId);
            return true;
        }
        catch (EntityNotFoundException)
        {
            return false;
        }
    }
}
