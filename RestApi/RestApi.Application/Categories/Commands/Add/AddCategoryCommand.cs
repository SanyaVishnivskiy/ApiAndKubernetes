namespace RestApi.Application.Categories.Commands.Add;

public class AddCategoryCommand : IRequest<string>
{
    public string Name { get; set; }
}
