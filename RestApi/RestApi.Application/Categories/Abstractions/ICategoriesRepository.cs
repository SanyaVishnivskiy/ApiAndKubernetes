namespace RestApi.Application.Categories.Abstractions;

public interface ICategoriesRepository
{
    Task<List<Category>> Get(CancellationToken cancellationToken);
    Task Add(Category category);
    Task Update(Category category);
    Task Delete(string categoryId);
    Task SaveChanges();
}
