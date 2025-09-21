using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IServiceManger
    {
        public IProductService ProductService { get; }
        public IBasketServices BasketServices { get; }
        public IAuthenticationServices AuthenticationServices { get; }
        public IOrderServices OrderServices  { get; }
        public IPaymentServices PaymentServices { get; }
    }
}
