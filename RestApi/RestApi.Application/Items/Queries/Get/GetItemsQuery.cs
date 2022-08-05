namespace RestApi.Application.Items.Queries.Get;

public class GetItemsQuery : IRequest<List<Item>>
{
    public string CategoryId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
