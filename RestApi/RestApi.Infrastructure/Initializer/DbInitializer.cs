using RestApi.Application.Categories.Abstractions;
using RestApi.Application.Items.Abstractions;
using RestApi.Infrastructure.Categories;
using RestApi.Infrastructure.EF;
using RestApi.Infrastructure.Items;

namespace RestApi.Infrastructure.Initializer;

public class DbInitializer
{
    private readonly CategoriesDbContext _context;
    private readonly CategoriesRepository _categoriesRepository;
    private readonly ItemsRepository _itemsRepository;

    private const string LaptopsCategoryId = "F7728D20-BE30-4BCA-ACC2-5F491AA37234";
    private const string PCsCategoryId = "9B1EF3D9-1CF7-443D-A2EE-9A26BD70D7D9";

    public DbInitializer(
        CategoriesDbContext context,
        ICategoriesRepository repository,
        IItemsRepository itemsRepository)
    {
        _context = context;
        _categoriesRepository = (CategoriesRepository)repository;
        _itemsRepository = (ItemsRepository)itemsRepository;
    }

    public async Task Initialize()
    {
        _context.Database.EnsureCreated();

        var categories = new List<Category>
        {
            new Category
            {
                Id = LaptopsCategoryId,
                Name = "Laptops"
            },
            new Category
            {
                Id = PCsCategoryId,
                Name = "PCs"
            }
        };

        await AddRangeIfNotExists(_categoriesRepository, categories, x => new PK(x.Id));

        var items = new List<Item>
        {
            new Item
            {
                Id = "5237BED7-6CC2-4BB4-BB46-C74D7328FF34",
                Name = "HP EliteBook",
                CategoryId = LaptopsCategoryId,
            },
            new Item
            {
                Id = "F9166BF1-B357-4C0D-9F83-151D6C2DE9CF",
                Name = "Dell G3",
                CategoryId = LaptopsCategoryId,
            },
            new Item
            {
                Id = "31AE0EFA-F694-4BFA-BB9F-A4B225E57BDE",
                Name = "Apple iMac",
                CategoryId = PCsCategoryId,
            }
        };

        await AddRangeIfNotExists(_itemsRepository, items, x => new CompositePK(x.Id, x.CategoryId));
    }

    private async Task AddRangeIfNotExists<T>(IRepository<T> repository, List<T> items, Func<T, PK> idSelector) where T: class
    {
        foreach (var item in items)
        {
            await AddIfNotExists(repository, item, idSelector);
        }
    }

    private async Task AddIfNotExists<T>(IRepository<T> repository, T obj, Func<T, PK> idSelector) where T: class
    {
        var entity = await FindEntity(repository, obj, idSelector);
        if (entity is null)
        {
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
        }
    }

    private async Task<T> FindEntity<T>(IRepository<T> repository, T obj, Func<T, PK> idSelector) where T : class
    {
        try
        {
            return await repository.GetById(idSelector(obj), default);
        }
        catch (EntityNotFoundException)
        {
            return null!;
        }
    }
}
