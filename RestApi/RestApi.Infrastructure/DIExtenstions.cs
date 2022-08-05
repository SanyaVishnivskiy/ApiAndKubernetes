using Microsoft.Extensions.DependencyInjection;
using RestApi.Application.Categories.Abstractions;
using RestApi.Application.Configuration;
using RestApi.Application.Items.Abstractions;
using RestApi.Application.Settings.Abstractions;
using RestApi.Infrastructure.Categories;
using RestApi.Infrastructure.EF;
using RestApi.Infrastructure.Initializer;
using RestApi.Infrastructure.Items;
using RestApi.Infrastructure.Settings;

namespace RestApi.Infrastructure;

public static class DIExtenstions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CategoriesDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Categories")));

        services.AddTransient<ICategoriesRepository, CategoriesRepository>();
        services.AddTransient<IItemsRepository, ItemsRepository>();

        services.AddTransient<ISettingsGateway, SettingsGateway>();

        services.AddTransient<DbInitializer>();

        return services;
    }
}
