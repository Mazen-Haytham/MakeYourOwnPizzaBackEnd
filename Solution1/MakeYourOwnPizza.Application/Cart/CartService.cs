using System;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Abstractions.Persistence;
using MakeYourOwnPizza.Application.Cart.AddToCart;
using MakeYourOwnPizza.Application.Cart.GetCart;
using MakeYourOwnPizza.Application.Cart.UpdateCartItem;
using MakeYourOwnPizza.Application.Cart.RemoveCartItem;

namespace MakeYourOwnPizza.Application.Cart
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<GetCartResponse?> GetCartByUserIdAsync(Guid userId)
        {
            return await _cartRepository.GetCartByUserIdAsync(userId);
        }

        public async Task<AddToCartResponse> AddToCartAsync(Guid userId,
            AddToCartRequest request)
        {
            return await _cartRepository.AddToCartAsync(userId, request);
        }

        public async Task<UpdateCartItemResponse> UpdateCartItemAsync(Guid userId, Guid cartItemId, UpdateCartItemRequest request)
        {
            return await _cartRepository.UpdateCartItemAsync(userId, cartItemId, request);
        }

        public async Task<RemoveCartItemResponse> RemoveCartItemAsync(Guid userId, RemoveCartItemRequest request)
        {
            return await _cartRepository.RemoveCartItemAsync(userId, request);
        }
    }
}