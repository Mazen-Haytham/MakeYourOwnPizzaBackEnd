using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Abstractions.Persistence;

namespace MakeYourOwnPizza.Application.Orders
{
    public interface IOrderService
    {
        Task<ICollection<GetOrderResponse>> GetOrdersByUserIdAsync(Guid userId, bool isActive);
    }
}
