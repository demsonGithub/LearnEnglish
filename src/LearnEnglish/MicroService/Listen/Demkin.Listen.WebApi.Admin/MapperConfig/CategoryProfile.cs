namespace Demkin.Listen.WebApi.Admin.MapperConfig
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }
}