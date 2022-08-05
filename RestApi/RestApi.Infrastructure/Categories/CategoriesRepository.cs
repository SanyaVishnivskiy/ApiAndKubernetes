using RestApi.Application.Categories.Abstractions;
using RestApi.Infrastructure.EF;

namespace RestApi.Infrastructure.Categories;

internal class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
{
    public CategoriesRepository(CategoriesDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override Task<List<Category>> Get(CancellationToken cancellationToken)
    {
        return Set.Include(x => x.Items).ToListAsync(cancellationToken);
    }

    public Task Delete(string categoryId)
    {
        return Delete(new PK(categoryId));
    }

    public Task Update(Category category)
    {
        return Update(new PK(category.Id), category);
    }
}
