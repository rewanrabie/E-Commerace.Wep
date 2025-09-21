using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.InterFaceRepostory_Contracts_;
using DomainLayer.Models.BasketModule;
using ServicesAbstraction;
using Shared.DataTransferObjects.BasketModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serives
{
    public class BasketServices(IBasketRepository _basketRepository,IMapper _mapper) : IBasketServices
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomarBasket = _mapper.Map<BasketDto, CustomarBasket>(basket);
           var CreatedOrUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(CustomarBasket);
            if (CreatedOrUpdatedBasket is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can NO Create Or Update Basket Now , Try Again Later");
        }

        public async Task<bool> DeleteBasketAsync(string key) => await _basketRepository.DeleteBasketAsync(key);

        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var Basket = await _basketRepository.GetBasketAsync(key);
            if (Basket is not null)
                return _mapper.Map<CustomarBasket, BasketDto>(Basket);
            else
                throw new BasketNotFoundException(key);
        }
    }
}
