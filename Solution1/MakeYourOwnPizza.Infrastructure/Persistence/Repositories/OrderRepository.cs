using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MakeYourOwnPizza.Application.Abstractions.Persistence;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GetOrderDetailsResponse?> GetOrdersDetailsByUserIdAsync(Guid orderId)
        {
            return await _context.Order
                .Where(o => o.Id == orderId)
                .Select(o => new GetOrderDetailsResponse
                {
                    OrderId = o.Id,
                    TotalPrice = o.totalPrice,
                    PaymentMethod = o.paymentMethod,
                    CustomerPhone = o.user.phone,
                    status = o.orderStages.OrderByDescending(s => s.createdAt).ThenByDescending(s => s.Id).Select(s => s.stageType).FirstOrDefault() ?? string.Empty,
                    Pizzas = o.orderItems.Select(p => new GetPizzaDto
                    {
                        PizzaId = p.pizzaId,
                        PizzaName = p.pizza.name,
                        Price = p.pizza.price,
                        Ingredients = p.orderIngredients.Select(i => new GetOrderIngredientDto
                        {
                            IngredientId = i.ingredientId,
                            IngredientName = i.Ingredient.name,
                            Quantity = i.quantity
                        }).ToList()
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<GetOrderResponse>> GetOrdersByUserIdAsync(Guid userId, bool isActive)
        {
            return await _context.Order
                .Where(o => o.userId == userId && o.isActive == isActive)
                .Select(o => new GetOrderResponse
                {
                    OrderId = o.Id,
                    TotalPrice = o.totalPrice,
                    PizzaCount = o.orderItems.Count(),
                    CreatedAt = o.createdAt,
                    Status = o.orderStages.OrderByDescending(s => s.createdAt).ThenByDescending(s => s.Id).Select(s => s.stageType).FirstOrDefault() ?? string.Empty
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
