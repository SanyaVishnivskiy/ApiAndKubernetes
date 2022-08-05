namespace RestApi.Domain;

public class Item
{
    public const int IdMaxLength = 50;
    public const int NameMaxLength = 200;
    public const int CategoryIdMaxLength = 50;

    public string Id { get; set; }
    public string Name { get; set; }
    public string CategoryId { get; set; }
}
