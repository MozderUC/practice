using AutoMapper;
using Category = NetCoreMentoring.App.Models.Category;
using Product = NetCoreMentoring.App.Models.Product;
using Supplier = NetCoreMentoring.App.Models.Supplier;

namespace NetCoreMentoring.App.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, Core.Models.Category>().ReverseMap();

            CreateMap<Product, Core.Models.Product>().ReverseMap();

            CreateMap<Supplier, Core.Models.Supplier>().ReverseMap();
        }
    }
}