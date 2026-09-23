using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MakeYourOwnPizza.Application.Abstractions.Persistence;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GetOrderDetailsResponse?> GetOrdersDetailsAsync(Guid orderId)
        {
            return await _context.Order
                .Where(o => o.Id == orderId)
                .Select(o => new GetOrderDetailsResponse
                {
                    OrderId = o.Id.ToString(),
                    Status = o.orderStages.OrderByDescending(s => s.createdAt).ThenByDescending(s => s.Id).Select(s => s.stageType).FirstOrDefault() ?? string.Empty,
                    CreatedAt = o.createdAt,
                    Note = o.note ?? string.Empty,
                    PaymentMethod = (int)o.paymentMethod,
                    TotalPrice = o.totalPrice,
                    Customer = new CustomerDto
                    {
                        Name = o.user.firstName + " " + o.user.lastName,
                        Phone = o.user.phone,
                        Email = o.user.email
                    },
                    DeliveryAddress = new DeliveryAddressDto
                    {
                        Street = o.street ?? string.Empty,
                        District = o.district ?? string.Empty,
                        City = o.city ?? string.Empty,
                        Floor = o.floor ?? string.Empty,
                        Apartment = o.apartment ?? string.Empty,
                        Formatted = o.formattedAddress ?? string.Empty
                    },
                    Items = o.orderItems.Select(p => new OrderItemDto
                    {
                        Id = p.pizzaId.ToString(),
                        Name = p.pizza.name,
                        Size = p.size ?? "Large",
                        Price = p.pizza.price,
                        Quantity = (int)p.quantity,
                        Toppings = p.orderIngredients.Select(i => i.Ingredient.name).ToList()
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
                    CustomerName = (o.user.firstName + " " + o.user.lastName).Trim(),
                    TotalPrice = o.totalPrice,
                    PizzaCount = o.orderItems.Count(),
                    CreatedAt = o.createdAt,
                    Status = o.orderStages.OrderByDescending(s => s.createdAt).ThenByDescending(s => s.Id).Select(s => s.stageType).FirstOrDefault() ?? string.Empty
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<GetOrderResponse>> GetAllOrdersAsync(bool? isActive = null)
        {
            var query = _context.Order.AsQueryable();
            if (isActive.HasValue)
            {
                query = query.Where(o => o.isActive == isActive.Value);
            }

            return await query
                .Select(o => new GetOrderResponse
                {
                    OrderId = o.Id,
                    CustomerName = (o.user.firstName + " " + o.user.lastName).Trim(),
                    TotalPrice = o.totalPrice,
                    PizzaCount = o.orderItems.Count(),
                    CreatedAt = o.createdAt,
                    Status = o.orderStages.OrderByDescending(s => s.createdAt).ThenByDescending(s => s.Id).Select(s => s.stageType).FirstOrDefault() ?? string.Empty
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, string status)
        {
            var orderExists = await _context.Order.AnyAsync(o => o.Id == orderId);
            if (!orderExists) return false;

            var stage = new OrderStage
            {
                Id = Guid.NewGuid(),
                orderId = orderId,
                stageType = status,
                createdAt = System.DateTimeOffset.Now
            };

            _context.OrderStage.Add(stage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Guid> PlaceOrderAsync(Guid userId, CheckoutOrderRequest request)
        {
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                userId = userId,
                createdAt = System.DateTimeOffset.Now,
                paymentMethod = (Domain.Enums.PaymentMethod)request.PaymentMethod,
                totalPrice = request.TotalPrice,
                isActive = true
            };

            foreach (var itemReq in request.Items)
            {
                var pizza = new Pizza
                {
                    Id = Guid.NewGuid(),
                    name = itemReq.Name,
                    price = itemReq.Price
                };
                
                _context.Pizza.Add(pizza);

                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    orderId = orderId,
                    pizzaId = pizza.Id,
                    quantity = itemReq.Quantity,
                    price = itemReq.Price,
                    size = itemReq.Size,
                    description = itemReq.Description
                };
                
                _context.OrderItem.Add(orderItem);
            }

            var initialStage = new OrderStage
            {
                Id = Guid.NewGuid(),
                orderId = orderId,
                stageType = "Pending", // Or whatever the initial status should be
                createdAt = System.DateTimeOffset.Now
            };

            _context.OrderStage.Add(initialStage);
            _context.Order.Add(order);

            await _context.SaveChangesAsync();
            return orderId;
        }
    }
}
