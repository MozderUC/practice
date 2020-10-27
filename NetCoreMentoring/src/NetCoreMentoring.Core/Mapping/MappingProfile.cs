using AutoMapper;
using Category = NetCoreMentoring.Core.Models.Category;
using Product = NetCoreMentoring.Core.Models.Product;
using Supplier = NetCoreMentoring.Core.Models.Supplier;

namespace NetCoreMentoring.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, Data.Models.Category>().ReverseMap();

            CreateMap<Product, Data.Models.Product>().ReverseMap();

            CreateMap<Supplier, Data.Models.Supplier>().ReverseMap();
        }
    }
}