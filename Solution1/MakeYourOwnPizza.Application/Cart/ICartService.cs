using System;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Cart.AddToCart;
using MakeYourOwnPizza.Application.Cart.AddCartItem;
using MakeYourOwnPizza.Application.Cart.GetCart;
using MakeYourOwnPizza.Application.Cart.UpdateCartItem;
using MakeYourOwnPizza.Application.Cart.RemoveCartItem;

namespace MakeYourOwnPizza.Application.Cart
{
    public interface ICartService
    {
        Task<AddToCartResponse> AddToCartAsync(Guid userId, AddToCartRequest request);
        Task<CartItemResponse> AddCartItemAsync(Guid userId, AddCartItemRequest request);
        Task<GetCartResponse?> GetCartByUserIdAsync(Guid userId);
        Task<UpdateCartItemResponse> UpdateCartItemAsync(Guid userId, Guid cartItemId, UpdateCartItemRequest request);
        Task<RemoveCartItemResponse> RemoveCartItemAsync(Guid userId, RemoveCartItemRequest request);
    }
}