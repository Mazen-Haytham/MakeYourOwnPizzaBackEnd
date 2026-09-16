using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MakeYourOwnPizza.Application.Cart;
using MakeYourOwnPizza.Application.Cart.AddToCart;
using MakeYourOwnPizza.Application.Cart.AddCartItem;
using MakeYourOwnPizza.Application.Cart.GetCart;
using MakeYourOwnPizza.Application.Cart.UpdateCartItem;
using MakeYourOwnPizza.Application.Cart.RemoveCartItem;
using MakeYourOwnPizza.Presentation.Extensions;

namespace MakeYourOwnPizza.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<ActionResult<GetCartResponse>> GetCart()
        {
            var userId = User.GetUserId();

            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart == null)
                return NotFound("Cart not found.");

            return Ok(cart);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<ActionResult<AddToCartResponse>> AddToCart([FromBody] AddToCartRequest request)
        {
            var userId = User.GetUserId();

            var response = await _cartService.AddToCartAsync(userId, request);

            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("items")]
        public async Task<ActionResult<CartItemResponse>> AddCartItem([FromBody] AddCartItemRequest request)
        {
            var userId = User.GetUserId();

            var response = await _cartService.AddCartItemAsync(userId, request);

            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("{cartItemId}")]
        public async Task<ActionResult<UpdateCartItemResponse>> UpdateCartItem(Guid cartItemId, [FromBody] UpdateCartItemRequest request)
        {
            var userId = User.GetUserId();

            var response = await _cartService.UpdateCartItemAsync(userId, cartItemId, request);

            return Ok(response);
        }


        [Authorize(Roles = "Customer")]
        [HttpDelete("{cartItemId}")]
        public async Task<ActionResult<RemoveCartItemResponse>> RemoveCartItem(Guid cartItemId)
        {
            var userId = User.GetUserId();

            var request = new RemoveCartItemRequest { CartItemId = cartItemId };

            var response = await _cartService.RemoveCartItemAsync(userId, request);

            return Ok(response);
        }
    }
}