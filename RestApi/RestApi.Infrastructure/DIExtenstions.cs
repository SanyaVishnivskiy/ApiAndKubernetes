using RestApi.Application.Categories.Abstractions;
using RestApi.Application.Items.Abstractions;
using RestApi.Infrastructure.Categories;
using RestApi.Infrastructure.EF;
using RestApi.Infrastructure.Initializer;
using RestApi.Infrastructure.Items;

namespace RestApi.Infrastructure;

public static class DIExtenstions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CategoriesDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Categories")));

        services.AddTransient<ICategoriesRepository, CategoriesRepository>();
        services.AddTransient<IItemsRepository, ItemsRepository>();

        services.AddTransient<DbInitializer>();

        return services;
    }
}
