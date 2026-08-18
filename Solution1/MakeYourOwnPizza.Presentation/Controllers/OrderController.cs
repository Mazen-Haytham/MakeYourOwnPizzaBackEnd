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

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<ActionResult<ICollection<GetOrderResponse>>> GetOrdersByUserId([FromQuery] bool isActive)
        {
            var userId = User.GetUserId();
            var orders = await _orderService.GetOrdersByUserIdAsync(userId, isActive);
            if (orders == null || orders.Count == 0)
                return NotFound("No orders found for the user.");
            
            return Ok(orders);
        }
    }
}
