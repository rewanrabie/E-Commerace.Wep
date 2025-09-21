using Shared.DataTransferObjects.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.OrderDTO
{
    public class OrderToReturnDTo
    {
        public Guid Id { get; set; }
        public string buyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; }
        public AddressDTO Address { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public decimal deliveryCost { get; set; }
        public string status { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = [];
        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }
    }
}
