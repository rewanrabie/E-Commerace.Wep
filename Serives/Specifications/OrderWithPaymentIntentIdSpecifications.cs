using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serives.Specifications
{
     class OrderWithPaymentIntentIdSpecifications : BaseSpecifications<Order,Guid>
    {
        public OrderWithPaymentIntentIdSpecifications(string PaymentIntentId):base(O=>O.PaymentIntentId == PaymentIntentId)
        {
            
        }
    }
}
