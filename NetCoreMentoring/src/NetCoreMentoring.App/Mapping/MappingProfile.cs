using AutoMapper;
using NetCoreMentoring.App.Models;

namespace NetCoreMentoring.App.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryViewModel, Core.Models.Category>().ReverseMap();

            CreateMap<ProductViewModel, Core.Models.Product>().ReverseMap();

            CreateMap<SupplierViewModel, Core.Models.Supplier>().ReverseMap();
        }
    }
}