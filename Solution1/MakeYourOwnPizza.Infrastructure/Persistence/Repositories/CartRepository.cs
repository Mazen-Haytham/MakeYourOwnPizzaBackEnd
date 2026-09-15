using Microsoft.EntityFrameworkCore;
using MakeYourOwnPizza.Application.Abstractions.Persistence;
using MakeYourOwnPizza.Application.Cart;
using MakeYourOwnPizza.Application.Cart.GetCart;
using MakeYourOwnPizza.Application.Cart.AddToCart;
using MakeYourOwnPizza.Application.Cart.UpdateCartItem;
using MakeYourOwnPizza.Application.Cart.RemoveCartItem;
namespace MakeYourOwnPizza.Infrastructure.Persistence.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GetCartResponse?> GetCartByUserIdAsync(Guid userId)
        {
            return await _context.Cart
                .Where(c => c.UserId == userId)
                .Select(c => new GetCartResponse
                {
                    CartId = c.Id,

                    Items = c.Items.Select(item => new CartItemResponse
                    {
                        CartItemId = item.Id,
                        PizzaId = item.PizzaId,
                        Quantity = item.quantity,

                        Ingredients = item.Ingredients.Select(ingredient => new CartIngredientResponse
                        {
                            IngredientId = ingredient.IngredientId,
                            Quantity = ingredient.quantity
                        }).ToList()

                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<AddToCartResponse> AddToCartAsync(Guid userId, AddToCartRequest request)
        {
            var cart = await _context.Cart
                .Include(c => c.Items)
                    .ThenInclude(i => i.Ingredients)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Domain.Entities.Cart
                {
                    UserId = userId,
                    Items = new List<Domain.Entities.CartItem>()
                };
                _context.Cart.Add(cart);
            }

            var cartItem = new Domain.Entities.CartItem
            {
                PizzaId = request.PizzaId,
                quantity = request.Quantity,
                Ingredients = request.Ingredients.Select(i => new Domain.Entities.CartIngredient
                {
                    IngredientId = i.IngredientId,
                    quantity = i.Quantity
                }).ToList()
            };

            cart.Items.Add(cartItem);
            await _context.SaveChangesAsync();

            return new AddToCartResponse
            {
                CartItemId = cartItem.Id,
                PizzaId = cartItem.PizzaId,
                Quantity = cartItem.quantity,
                Ingredients = cartItem.Ingredients.Select(i => new CartIngredientResponse
                {
                    IngredientId = i.IngredientId,
                    Quantity = i.quantity
                }).ToList()
            };
        }

        public async Task<UpdateCartItemResponse> UpdateCartItemAsync(Guid userId, Guid cartItemId, UpdateCartItemRequest request)
        {
            var cart = await _context.Cart
                .Include(c => c.Items)
                    .ThenInclude(i => i.Ingredients)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                throw new Exception("Cart not found.");
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.Id == cartItemId);
            if (cartItem == null)
            {
                throw new Exception("Cart item not found.");
            }

            cartItem.quantity = request.Quantity;

            cartItem.Ingredients.Clear();
            foreach (var ingredient in request.Ingredients)
            {
                cartItem.Ingredients.Add(new Domain.Entities.CartIngredient
                {
                    IngredientId = ingredient.IngredientId,
                    quantity = ingredient.Quantity
                });
            }

            await _context.SaveChangesAsync();

            return new UpdateCartItemResponse
            {
                CartId = cart.Id,
                CartItemId = cartItem.Id,
                Quantity = cartItem.quantity,
                Ingredients = cartItem.Ingredients.Select(i => new CartIngredientResponse
                {
                    IngredientId = i.IngredientId,
                    Quantity = i.quantity
                }).ToList()
            };
        }

        public async Task<RemoveCartItemResponse> RemoveCartItemAsync(Guid userId, RemoveCartItemRequest request)
        {
            var cart = await _context.Cart
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                throw new Exception("Cart not found.");
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.Id == request.CartItemId);
            if (cartItem == null)
            {
                throw new Exception("Cart item not found.");
            }

            cart.Items.Remove(cartItem);
            await _context.SaveChangesAsync();

            return new RemoveCartItemResponse
            {
                CartItemId = cartItem.Id,
                CartId = cart.Id
            };
        }

        
        }
    }
