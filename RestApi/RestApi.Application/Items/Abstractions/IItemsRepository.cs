namespace RestApi.Application.Items.Abstractions;

public interface IItemsRepository
{
    Task<List<Item>> Get(ItemsFilterOptions options, CancellationToken cancellationToken);
    Task Add(Item item);
    Task Update(Item item);
    Task Delete(string itemId, string categoryId);
    Task SaveChanges();
}

public class ItemsFilterOptions
{
    public string CategoryId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
