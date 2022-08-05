namespace RestApi.Application.Items.Commands.Update;

public class UpdateItemModel
{
    public string Name { get; set; }
}

public class UpdateItemCommand : UpdateItemModel, IRequest<Unit>
{
    public string Id { get; set; }
    public string CategoryId { get; set; }
}
