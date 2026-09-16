using System;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Cart.AddToCart;
using MakeYourOwnPizza.Application.Cart.AddCartItem;
using MakeYourOwnPizza.Application.Cart.GetCart;
using MakeYourOwnPizza.Application.Cart.UpdateCartItem;
using MakeYourOwnPizza.Application.Cart.RemoveCartItem;

namespace MakeYourOwnPizza.Application.Abstractions.Persistence
{
    public interface ICartRepository
    {
        Task<GetCartResponse?> GetCartByUserIdAsync(Guid userId);
        Task<AddToCartResponse> AddToCartAsync(Guid userId, AddToCartRequest request);
        Task<CartItemResponse> AddCartItemAsync(Guid userId, AddCartItemRequest request);
        Task<UpdateCartItemResponse> UpdateCartItemAsync(Guid userId, Guid cartItemId, UpdateCartItemRequest request);
        Task<RemoveCartItemResponse> RemoveCartItemAsync(Guid userId, RemoveCartItemRequest request);
    }
}