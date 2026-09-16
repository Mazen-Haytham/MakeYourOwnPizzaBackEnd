using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Abstractions.Persistence;

namespace MakeYourOwnPizza.Application.Orders
{
    public interface IOrderService
    {
        Task<ICollection<GetOrderResponse>> GetOrdersByUserIdAsync(Guid userId, bool isActive);
        Task<GetOrderDetailsResponse?> GetOrderDetailsAsync(Guid orderId);
        Task<bool> UpdateOrderStatusAsync(Guid orderId, string status);
        Task<Guid> PlaceOrderAsync(Guid userId, MakeYourOwnPizza.Application.Abstractions.Persistence.CheckoutOrderRequest request);
    }
}
