namespace RestApi.Application.Items.Commands.Update;

public class UpdateItemsCommandHandler : IRequestHandler<UpdateItemCommand>
{
    private readonly IItemsRepository _repository;
    private readonly IMapper _mapper;

    public UpdateItemsCommandHandler(IItemsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Item>(request);

        var udpated = await TryUpdate(item);
        if (udpated)
        {
            await _repository.SaveChanges();
        }

        return Unit.Value;
    }

    private async Task<bool> TryUpdate(Item item)
    {
        try
        {
            await _repository.Update(item);
            return true;
        }
        catch (EntityNotFoundException)
        {
            return false;
        }
    }
}
