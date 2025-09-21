using DomainLayer.InterFaceRepostory_Contracts_;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Serives
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(Serives.AssemplyReferance).Assembly);
            Services.AddScoped<IServiceManger, SerivesManagerWithFactoryDelegte>();


            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(Provider => () => Provider.GetRequiredService<IProductService>());


            Services.AddScoped<IOrderServices, OrderServices>();
            Services.AddScoped<Func<IOrderServices>>(Provider => () => Provider.GetRequiredService<IOrderServices>());


            Services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            Services.AddScoped<Func<IAuthenticationServices>>(Provider => () => Provider.GetRequiredService<IAuthenticationServices>());


            Services.AddScoped<IBasketServices, BasketServices>();
            Services.AddScoped<Func<IBasketServices>>(Provider => () => Provider.GetRequiredService<IBasketServices>());

            Services.AddScoped<ICacheServices, CacheService>();

            Services.AddScoped<IPaymentServices, PaymentServices>();
            Services.AddScoped<Func<IPaymentServices>>(Provider => () => Provider.GetRequiredService<IPaymentServices>());

            return Services;
        }
    }
}
