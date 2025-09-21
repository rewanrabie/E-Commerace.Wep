using DomainLayer.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.InterFaceRepostory_Contracts_
{
    public interface IBasketRepository
    {
        Task<CustomarBasket?> GetBasketAsync(string key);
        Task<CustomarBasket?> CreateOrUpdateBasketAsync(CustomarBasket basket, TimeSpan? TimeLive=null);
        Task<bool> DeleteBasketAsync(string id);

    }
}
