using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DataTransferObjects.IdentityDTOs;
using Shared.DataTransferObjects.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serives.MappingProfiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTO,OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDTo>()
                .ForMember(D=>D.DeliveryMethod,O=>O.MapFrom(S=>S.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D=>D.PictureUrl,O=>O.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod,DeliveryMethodDTo>();
        }
    }
}
