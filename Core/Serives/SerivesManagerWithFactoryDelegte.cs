using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serives
{
    public class SerivesManagerWithFactoryDelegte(Func<IProductService> ProductFactory,
        Func<IBasketServices> BasketFactory,
        Func<IAuthenticationServices> AuthenticationFactory,
        Func<IOrderServices> OrderFactory ,
        Func<IPaymentServices> PaymentFactory
        ) : IServiceManger
    {
        public IProductService ProductService => ProductFactory.Invoke();

        public IBasketServices BasketServices => BasketFactory.Invoke();

        public IAuthenticationServices AuthenticationServices => AuthenticationFactory.Invoke();
 
        public IOrderServices OrderServices => OrderFactory.Invoke();

        public IPaymentServices PaymentServices => PaymentFactory.Invoke();
    }
}
