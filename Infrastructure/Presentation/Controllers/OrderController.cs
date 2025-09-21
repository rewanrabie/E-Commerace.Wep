using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObjects.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class OrderController(IServiceManger _serviceManger) : ApiBaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTo>> CreateOrder(OrderDTo orderDTo)
        {
            var Order = await _serviceManger.OrderServices.CreateOrderAsync(orderDTo,GetEmailFromToken());
            return Ok(Order);
        }
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTo>>> GetDeliveryMethods()
        {
            var DeliveryMethods = await _serviceManger.OrderServices.GetDeliveryMethodAsync();
            return Ok(DeliveryMethods);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTo>>> GetAllOrders()
        {
            var Orders = await _serviceManger.OrderServices.GetAllOrderAsync(GetEmailFromToken());
            return Ok(Orders);
        }
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDTo>> GetOrderById(Guid id)
        {
            var Order = await _serviceManger.OrderServices.GetOrderByIdAsync(id);
            return Ok(Order);
        }

    }
}
