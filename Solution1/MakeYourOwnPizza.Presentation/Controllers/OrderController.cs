using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Orders;
using MakeYourOwnPizza.Application.Abstractions.Persistence;
using MakeYourOwnPizza.Presentation.Extensions;

namespace MakeYourOwnPizza.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "Customer,Manager")]
        [HttpGet]
        public async Task<ActionResult<ICollection<GetOrderResponse>>> GetOrders([FromQuery] bool? isActive)
        {
            if (User.IsInRole("Manager"))
            {
                var orders = await _orderService.GetAllOrdersAsync(isActive);
                return Ok(orders ?? new List<GetOrderResponse>());
            }
            else
            {
                var userId = User.GetUserId();
                var orders = await _orderService.GetOrdersByUserIdAsync(userId, isActive ?? true);
                return Ok(orders ?? new List<GetOrderResponse>());
            }
        }
        [Authorize(Roles="Customer,Manager")]
        [HttpGet("{orderId:guid}")]
        public async Task<ActionResult<GetOrderDetailsResponse?>> GetOrderDetails(Guid orderId)
        {
            var order=await _orderService.GetOrderDetailsAsync(orderId);
            if (order == null) return NotFound("No Order Found");
            return Ok(order);
        }

        [Authorize(Roles = "Manager,Delivery")]
        [HttpPut("{orderId:guid}/status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid orderId, [FromBody] MakeYourOwnPizza.Application.Abstractions.Persistence.UpdateOrderStatusRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Status))
                return BadRequest("Status cannot be empty.");

            var success = await _orderService.UpdateOrderStatusAsync(orderId, request.Status);
            if (!success) return NotFound("Order not found.");

            return Ok();
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("{orderId:guid}/{driverId:guid}")]
        public async Task<IActionResult> AssignDriver(Guid orderId, Guid driverId, [FromServices] MakeYourOwnPizza.Infrastructure.Persistence.AppDbContext context)
        {
            var order = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(context.Order, o => o.Id == orderId);
            if (order == null) return NotFound("Order not found.");

            order.driverId = driverId;
            await context.SaveChangesAsync();
            return Ok(new { message = "Driver assigned successfully." });
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] CheckoutOrderRequest request)
        {
            if (request == null || request.Items == null || request.Items.Count == 0)
                return BadRequest("Order request or items cannot be null or empty.");

            var userId = User.GetUserId();
            var orderId = await _orderService.PlaceOrderAsync(userId, request);

            return CreatedAtAction(nameof(GetOrderDetails), new { orderId = orderId }, new { OrderId = orderId });
        }
    }
}
