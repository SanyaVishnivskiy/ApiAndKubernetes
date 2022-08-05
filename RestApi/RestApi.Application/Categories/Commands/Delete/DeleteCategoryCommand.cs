namespace RestApi.Application.Categories.Commands.Delete;

public class DeleteCategoryCommand : IRequest<Unit>
{
    public string Id { get; set; }
}
