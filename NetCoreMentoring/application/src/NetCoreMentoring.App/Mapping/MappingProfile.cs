using System.Collections.Generic;
using AutoMapper;
using NetCoreMentoring.App.Models;
using NetCoreMentoring.Core.Models;

namespace NetCoreMentoring.App.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryViewModel, Core.Models.Category>().ReverseMap();

            CreateMap<ProductViewModel, Core.Models.Product>().ReverseMap();

            CreateMap<SupplierViewModel, Core.Models.Supplier>().ReverseMap();

            CreateMap<CategoryPictureViewModel, Core.Models.Category>().ReverseMap();

            CreateMap<ProductAndCategoriesViewModel, Core.Models.ProductAndCategories>().ReverseMap();

            CreateMap<IEnumerable<Category>, ProductAndCategoriesViewModel>()
                .ForMember(
                    destination => destination.Categories,
                    option => option.MapFrom(source => source));
        }
    }
}