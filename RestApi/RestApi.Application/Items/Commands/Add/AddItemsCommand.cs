namespace RestApi.Application.Items.Commands.Add;

public class AddItemModel
{
    public string Name { get; set; }
}

public class AddItemCommand : AddItemModel, IRequest<string>
{
    public string CategoryId { get; set; }
}
