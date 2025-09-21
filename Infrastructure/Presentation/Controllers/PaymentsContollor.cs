using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObjects.BasketModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class PaymentsContollor(IServiceManger _serviceManger) : ApiBaseController
    {
        [Authorize]
        [HttpPost("{BasketId}")]

        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var Basket = await _serviceManger.PaymentServices.CreateOrUpdatePaymentIntentAsync(BasketId);
            return Ok(Basket);
        }

    }
}
