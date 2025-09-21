using AutoMapper;
using DomainLayer.InterFaceRepostory_Contracts_;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serives
{
    public class ServiceMangers(IUnitOfWork unitOfWork,IMapper mapper, IBasketRepository basketRepository, UserManager<ApplicationUser> userManager 
        , IConfiguration _configuration) 
    {
        private readonly Lazy<IProductService> _LazyproductService = new Lazy<IProductService>(() => new ProductService(unitOfWork,mapper));
        public IProductService ProductService => _LazyproductService.Value;
        private readonly Lazy<IBasketServices> _LazyBasketService = new Lazy<IBasketServices>(() => new BasketServices(basketRepository,mapper));
        private readonly Lazy<IAuthenticationServices> _LazyAuthenticationServices = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager , _configuration,mapper));
        private readonly Lazy<IOrderServices> _LazyOrderServices = new Lazy<IOrderServices>(() => new OrderServices(mapper, basketRepository, unitOfWork));
        public IBasketServices BasketServices => _LazyBasketService.Value;

        public IAuthenticationServices AuthenticationServices => _LazyAuthenticationServices.Value;

        public IOrderServices OrderServices => _LazyOrderServices.Value;
    }
}
