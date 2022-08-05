using RestApi.Application.Items.Abstractions;
using RestApi.Infrastructure.EF;

namespace RestApi.Infrastructure.Items;

internal class ItemsRepository : GenericRepository<Item>, IItemsRepository
{
    public ItemsRepository(CategoriesDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public Task<List<Item>> Get(ItemsFilterOptions options, CancellationToken cancellationToken)
    {
        var query = Set.Where(x => x.CategoryId == options.CategoryId);
        var paginatedQuery = ApplyPagination(query, options.PageNumber, options.PageSize);
        return paginatedQuery.ToListAsync(cancellationToken);
    }

    private IQueryable<Item> ApplyPagination(IQueryable<Item> query, int? pageNumber, int? pageSize)
    {
        if (pageNumber is null && pageSize is null)
        {
            return query;
        }

        var skipQuery = query.Skip((pageSize ?? 0) * ((pageNumber - 1) ?? 0 ));
        if (pageSize is not null)
        {
            return skipQuery.Take(pageSize.Value);
        }

        return skipQuery;
    }

    public override async Task<Item> GetById(PK pk, CancellationToken cancellationToken = default)
    {
        if (pk is CompositePK key)
        {
            var entity = await Set.FirstOrDefaultAsync(
                x => x.Id == key.Id && x.CategoryId == key.AdditionalId,
                cancellationToken);

            if (entity is null)
            {
                throw new EntityNotFoundException(typeof(Item), pk.Id);
            }

            return entity;
        }

        return await base.GetById(pk, cancellationToken);
    }

    public Task Delete(string itemId, string categoryId)
    {
        return Delete(new CompositePK(itemId, categoryId));
    }

    public Task Update(Item item)
    {
        return Update(new CompositePK(item.Id, item.CategoryId), item);
    }
}
