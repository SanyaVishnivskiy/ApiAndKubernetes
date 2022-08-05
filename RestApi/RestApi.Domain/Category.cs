namespace RestApi.Domain;

public class Category
{
    public const int IdMaxLength = 50;
    public const int NameMaxLength = 200;

    public string Id { get; set; }
    public string Name { get; set; }
    public List<Item> Items { get; set; }
}
