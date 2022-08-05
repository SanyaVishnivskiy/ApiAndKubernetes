namespace RestApi.Application.Categories.Commands.Update;

public class UpdateCategoryModel
{
    public string Name { get; set; }
}

public class UpdateCategoryCommand : UpdateCategoryModel, IRequest<Unit>
{
    public string Id { get; set; }
}
