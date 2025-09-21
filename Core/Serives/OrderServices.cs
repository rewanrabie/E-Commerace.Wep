using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.InterFaceRepostory_Contracts_;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Serives.Specifications;
using Serives.Specifications.OrderMuduleSpecification;
using ServicesAbstraction;
using Shared.DataTransferObjects.IdentityDTOs;
using Shared.DataTransferObjects.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serives
{
    public class OrderServices(IMapper _mapper, IBasketRepository _basketRepository,IUnitOfWork _unitOfWork) : IOrderServices
    {
        public async Task<OrderToReturnDTo> CreateOrderAsync(OrderDTo orderDTo, string Email)
        {
            var OrderAddress = _mapper.Map<AddressDTO, OrderAddress>(orderDTo.ShipToAddress);

            var Basket = await _basketRepository.GetBasketAsync(orderDTo.BasketId) 
                ?? throw new BasketNotFoundException(orderDTo.BasketId);

            ArgumentNullException.ThrowIfNullOrEmpty(Basket.PaymentIntentId);

            var OrderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var OrderSpec = new OrderWithPaymentIntentIdSpecifications(Basket.PaymentIntentId);
            var ExistingOrder =await OrderRepo.GetByIdAsync(OrderSpec);
            if (ExistingOrder is not null) OrderRepo.Remove(ExistingOrder);

            List<OrderItem> orderItems = [];

            var ProductRepo = _unitOfWork.GetRepository<Product,int>();

            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                orderItems.Add(CreateOrderItem(item, Product));
            }

            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDTo.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDTo.DeliveryMethodId);

            var SubTotal = orderItems.Sum(I=>I.Quentity * I.Price);
            var Order = new Order(Email,OrderAddress,DeliveryMethod,orderItems,SubTotal,Basket.PaymentIntentId);

           await _unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Order, OrderToReturnDTo>(Order);
        }

        private static OrderItem CreateOrderItem(BasketItem item, Product Product)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrdered() { ProductId = Product.Id, PictureUrl = Product.PictureUrl, ProductName = Product.Name },
                                Price = Product.Price,
                Quentity = item.Quantity
            };
        }

        public async Task<IEnumerable<DeliveryMethodDTo>> GetDeliveryMethodAsync()
        {
            var DeliverMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDTo>>(DeliverMethods);
        }

        public async Task<IEnumerable<OrderToReturnDTo>> GetAllOrderAsync(string Email)
        {
            var Spec = new OrderSpecification(Email);
            var Orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDTo>>(Orders);
        }
        public async Task<OrderToReturnDTo> GetOrderByIdAsync(Guid Id)
        {
            var Spec = new OrderSpecification(Id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec);
            return _mapper.Map<Order, OrderToReturnDTo>(Order);
        }
    }
}
