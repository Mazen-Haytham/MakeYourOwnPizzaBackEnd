using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeYourOwnPizza.Infrastructure.Persistence;
using MakeYourOwnPizza.Presentation.Extensions;
using MakeYourOwnPizza.Application.Orders;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IOrderService _orderService;

        public DeliveryController(AppDbContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        [Authorize(Roles = "Delivery,Manager")]
        [HttpGet("assigned")]
        public async Task<IActionResult> GetAssignedDeliveries([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var userId = User.GetUserId();
            var driver = await _context.Driver.FirstOrDefaultAsync(d => d.UserId == userId);

            if (driver == null && User.IsInRole("Delivery"))
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null)
                {
                    driver = new Driver
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        Zone = "Cairo",
                        Status = Domain.Enums.DriverStatus.Available
                    };
                    _context.Driver.Add(driver);
                    await _context.SaveChangesAsync();
                }
            }

            IQueryable<Order> query = _context.Order
                .Include(o => o.user)
                .Include(o => o.orderItems)
                    .ThenInclude(oi => oi.pizza)
                .Include(o => o.orderItems)
                    .ThenInclude(oi => oi.orderIngredients)
                        .ThenInclude(i => i.Ingredient)
                .Include(o => o.orderStages);

            if (User.IsInRole("Delivery"))
            {
                if (driver != null)
                {
                    // Strictly show ONLY orders assigned to this specific driver
                    query = query.Where(o => o.driverId == driver.Id);
                }
                else
                {
                    query = query.Where(o => false);
                }
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(o => o.createdAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new
                {
                    CustomerName = o.user != null ? (o.user.firstName + " " + o.user.lastName).Trim() : "Customer",
                    CustomerPhone = o.user != null ? o.user.phone : "",
                    CustomerAddress = o.formattedAddress ?? (o.street + " " + o.city).Trim(),
                    OrderId = o.Id.ToString(),
                    OrderTime = o.createdAt.ToString("o"),
                    OrderCreationTime = o.createdAt.ToString("o"),
                    PizzaCount = o.orderItems.Count,
                    TotalPrice = o.totalPrice,
                    status = o.orderStages.OrderByDescending(s => s.createdAt).ThenByDescending(s => s.Id).Select(s => s.stageType).FirstOrDefault() ?? "Waiting For Delivery",
                    paymentMethod = (int)o.paymentMethod,
                    notes = o.note ?? "",
                    pizzas = o.orderItems.Select(p => new
                    {
                        pizzaId = p.pizzaId.ToString(),
                        pizzaName = p.pizza != null ? p.pizza.name : "Custom Pizza",
                        price = p.price,
                        ingredients = p.orderIngredients.Select(ing => new
                        {
                            ingredientId = ing.ingredientId.ToString(),
                            ingredientName = ing.Ingredient != null ? ing.Ingredient.name : "",
                            quantity = 1
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();

            return Ok(new
            {
                items = items,
                totalCount = totalCount
            });
        }

        [Authorize(Roles = "Delivery,Manager")]
        [HttpGet("{deliveryId:guid}")]
        public async Task<IActionResult> GetDeliveryDetails(Guid deliveryId)
        {
            var order = await _context.Order
                .Include(o => o.user)
                .Include(o => o.orderItems)
                    .ThenInclude(oi => oi.pizza)
                .Include(o => o.orderItems)
                    .ThenInclude(oi => oi.orderIngredients)
                        .ThenInclude(i => i.Ingredient)
                .Include(o => o.orderStages)
                .FirstOrDefaultAsync(o => o.Id == deliveryId);

            if (order == null) return NotFound("Delivery order not found.");

            var details = new
            {
                CustomerName = order.user != null ? (order.user.firstName + " " + order.user.lastName).Trim() : "Customer",
                CustomerPhone = order.user != null ? order.user.phone : "",
                CustomerAddress = order.formattedAddress ?? (order.street + " " + order.city).Trim(),
                OrderId = order.Id.ToString(),
                OrderTime = order.createdAt.ToString("o"),
                OrderCreationTime = order.createdAt.ToString("o"),
                PizzaCount = order.orderItems.Count,
                TotalPrice = order.totalPrice,
                status = order.orderStages.OrderByDescending(s => s.createdAt).ThenByDescending(s => s.Id).Select(s => s.stageType).FirstOrDefault() ?? "Waiting For Delivery",
                paymentMethod = (int)order.paymentMethod,
                notes = order.note ?? "",
                pizzas = order.orderItems.Select(p => new
                {
                    pizzaId = p.pizzaId.ToString(),
                    pizzaName = p.pizza != null ? p.pizza.name : "Custom Pizza",
                    price = p.price,
                    ingredients = p.orderIngredients.Select(ing => new
                    {
                        ingredientId = ing.ingredientId.ToString(),
                        ingredientName = ing.Ingredient != null ? ing.Ingredient.name : "",
                        quantity = 1
                    }).ToList()
                }).ToList()
            };

            return Ok(details);
        }

        public class UpdateDeliveryStatusRequest
        {
            public string Status { get; set; } = string.Empty;
        }

        [Authorize(Roles = "Delivery,Manager")]
        [HttpPut("{deliveryId:guid}/status")]
        public async Task<IActionResult> UpdateDeliveryStatus(Guid deliveryId, [FromBody] UpdateDeliveryStatusRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Status))
                return BadRequest("Status cannot be empty.");

            var success = await _orderService.UpdateOrderStatusAsync(deliveryId, request.Status);
            if (!success) return NotFound("Delivery order not found.");

            return await GetDeliveryDetails(deliveryId);
        }
    }
}
