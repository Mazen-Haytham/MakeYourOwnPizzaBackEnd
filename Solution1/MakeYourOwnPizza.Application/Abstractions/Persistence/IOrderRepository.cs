using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Application.Abstractions.Persistence
{
    public interface IOrderRepository
    {
        Task<GetOrderDetailsResponse?> GetOrdersDetailsAsync(Guid orderId);
        Task<ICollection<GetOrderResponse>> GetOrdersByUserIdAsync(Guid userId, bool isActive);
        Task<bool> UpdateOrderStatusAsync(Guid orderId, string status);
        Task<Guid> PlaceOrderAsync(Guid userId, CheckoutOrderRequest request);
    }
}
