using AutoMapper;
using DomainLayer.Models.ProductModule;
using Shared.DataTransferObjects.ProductModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Serives.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.ProductBrand, Options => Options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.ProductType, Options => Options.MapFrom(src => src.ProductType.Name))
               .ForMember(dist => dist.PictureUrl, Options => Options.MapFrom<PictureUrlResolver>());


            CreateMap<ProductType,TypeDto>();
            CreateMap<ProductBrand,BrandDto>();
        }
    }
}
