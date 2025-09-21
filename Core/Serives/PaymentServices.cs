using DomainLayer.Exceptions;
using DomainLayer.InterFaceRepostory_Contracts_;
using Microsoft.Extensions.Configuration;
using ServicesAbstraction;
using Shared.DataTransferObjects.BasketModuleDto;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Product = DomainLayer.Models.ProductModule.Product;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModule;
using AutoMapper;

namespace Serives
{
    public class PaymentServices(IConfiguration _configuration,
        IBasketRepository _basketRepository,
        IUnitOfWork _unitOfWork,
        IMapper _mapper ) : IPaymentServices
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var Basket = await _basketRepository.GetBasketAsync(BasketId) ?? throw new BasketNotFoundException(BasketId);

            var ProductRepo = _unitOfWork.GetRepository<Product,int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                item.Price = Product.Price;
            }
            ArgumentNullException.ThrowIfNull(Basket.DeliveryMethodId);
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(Basket.DeliveryMethodId.Value);

            Basket.ShippingPrice = DeliveryMethod.Cost;

            var BasketAmout = (long)(Basket.Items.Sum(item => item.Price * item.Quantity) + DeliveryMethod.Cost)*100;

            var PaymentService = new PaymentIntentService();

            if (Basket.PaymentIntentId is null)
            {
                var Options = new PaymentIntentCreateOptions()
                {
                    Amount = BasketAmout,
                    Currency="USD",
                    PaymentMethodTypes = ["card"],
                };
                var PaymentIntent = await PaymentService.CreateAsync(Options);
                Basket.PaymentIntentId = PaymentIntent.Id;
                Basket.ClientSecret = PaymentIntent.ClientSecret;
            }
            else
            {
                var Options = new PaymentIntentUpdateOptions() { Amount = BasketAmout };
                await PaymentService.UpdateAsync(Basket.PaymentIntentId,Options);
            }
            await _basketRepository.CreateOrUpdateBasketAsync(Basket);
            return _mapper.Map<BasketDto>(Basket);
        }
    }
}
