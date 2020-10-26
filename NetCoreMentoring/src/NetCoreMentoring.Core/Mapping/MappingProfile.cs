using AutoMapper;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Data.Models;
using Category = NetCoreMentoring.Core.Models.Category;
using Supplier = NetCoreMentoring.Core.Models.Supplier;

namespace NetCoreMentoring.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, Data.Models.Category>().ReverseMap();

            CreateMap<Products, Product>().ReverseMap();

            CreateMap<Supplier, Data.Models.Supplier>().ReverseMap();
        }
    }
}