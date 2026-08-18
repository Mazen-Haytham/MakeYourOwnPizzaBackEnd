using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Application.Abstractions.Persistence
{
    public interface IOrderRepository
    {
        Task<GetOrderDetailsResponse?> GetOrdersDetailsByUserIdAsync(Guid orderId);
        Task<ICollection<GetOrderResponse>> GetOrdersByUserIdAsync(Guid userId, bool isActive);
    }
}
