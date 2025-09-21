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
    public class BasketController(IServiceManger _serviceManger) : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var Basket = await _serviceManger.BasketServices.GetBasketAsync(key);
            return Ok(Basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManger.BasketServices.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        [HttpDelete("{Key}")] 
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var Result = await _serviceManger.BasketServices.DeleteBasketAsync(Key);
            return Ok(Result);
        }

    }
}
