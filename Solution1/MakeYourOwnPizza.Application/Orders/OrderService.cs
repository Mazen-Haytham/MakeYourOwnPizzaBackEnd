using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Abstractions.Persistence;

namespace MakeYourOwnPizza.Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ICollection<GetOrderResponse>> GetOrdersByUserIdAsync(Guid userId, bool isActive)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(userId, isActive);
        }
    }
}
