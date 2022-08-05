namespace RestApi.Application.Items.Commands.Delete;

public class DeleteItemCommand : IRequest<Unit>
{
    public string Id { get; set; }
    public string CategoryId { get; set; }
}
