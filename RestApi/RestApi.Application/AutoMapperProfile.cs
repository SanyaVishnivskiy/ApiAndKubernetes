using RestApi.Application.Categories.Commands.Update;
using RestApi.Application.Items.Commands.Add;
using RestApi.Application.Items.Commands.Update;

namespace RestApi.Application;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Category, Category>();
        CreateMap<AddCategoryCommand, Category>();
        CreateMap<UpdateCategoryCommand, Category>();
        CreateMap<UpdateCategoryModel, UpdateCategoryCommand>();

        CreateMap<Item, Item>();
        CreateMap<AddItemCommand, Item>();
        CreateMap<AddItemModel, AddItemCommand>();
        CreateMap<UpdateItemCommand, Item>();
        CreateMap<UpdateItemModel, UpdateItemCommand>();
    }
}
