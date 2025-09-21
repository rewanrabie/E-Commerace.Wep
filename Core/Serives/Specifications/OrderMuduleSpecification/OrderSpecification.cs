using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serives.Specifications.OrderMuduleSpecification
{
     class OrderSpecification: BaseSpecifications<Order,Guid>
    {
        public OrderSpecification(string Email):base(O=>O.UserEmail==Email)
        {
            AddInclude(O=>O.DeliveryMethod);
            AddInclude(O=>O.Items);
            AddOrderByDescending(O=>O.OrderDate);
        }
        public OrderSpecification(Guid id) : base(O=>O.Id==id)
        {
            AddInclude(O=>O.DeliveryMethod);
            AddInclude(O=>O.Items);
        }
    }
}
