using Shared.DataTransferObjects.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IOrderServices
    {
        Task<OrderToReturnDTo> CreateOrderAsync(OrderDTo orderDTo, string Email);

        Task<IEnumerable<DeliveryMethodDTo>> GetDeliveryMethodAsync();

        Task<IEnumerable<OrderToReturnDTo>> GetAllOrderAsync(string Email);
        Task<OrderToReturnDTo> GetOrderByIdAsync(Guid Id);
    }
}
