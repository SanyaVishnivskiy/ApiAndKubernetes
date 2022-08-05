namespace RestApi.Application.Items.Commands.Add;

public class AddItemsCommandHandler : IRequestHandler<AddItemCommand, string>
{
    private readonly IItemsRepository _repository;
    private readonly IMapper _mapper;

    public AddItemsCommandHandler(IItemsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<string> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Item>(request);
        var id = Guid.NewGuid().ToString();
        item.Id = id;

        await _repository.Add(item);
        await _repository.SaveChanges();

        return id;
    }
}
