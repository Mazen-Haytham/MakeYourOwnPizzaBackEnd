using Microsoft.EntityFrameworkCore;
using MakeYourOwnPizza.Application.Abstractions.Persistence;
using MakeYourOwnPizza.Application.Cart;
using MakeYourOwnPizza.Application.Cart.GetCart;
using MakeYourOwnPizza.Application.Cart.AddToCart;
using MakeYourOwnPizza.Application.Cart.AddCartItem;
using MakeYourOwnPizza.Application.Cart.UpdateCartItem;
using MakeYourOwnPizza.Application.Cart.RemoveCartItem;
using MakeYourOwnPizza.Domain.Entities;

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
                        Id = item.Id,
                        CartItemId = item.Id,
                        PizzaId = item.PizzaId,
                        Name = item.Name ?? (item.Pizza != null ? item.Pizza.name : "Custom Pizza"),
                        Size = item.Size ?? "Medium",
                        Description = item.Description ?? (item.Ingredients.Any() 
                            ? string.Join(", ", item.Ingredients.Select(i => i.Ingredients.name)) 
                            : "Custom pizza"),
                        Price = item.Price > 0 ? item.Price : (item.Pizza != null ? item.Pizza.price : 0m),
                        Quantity = item.quantity,
                        Image = item.Image ?? "pizza.png",

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
                PizzaId = cartItem.PizzaId ?? Guid.Empty,
                Quantity = cartItem.quantity,
                Ingredients = cartItem.Ingredients.Select(i => new CartIngredientResponse
                {
                    IngredientId = i.IngredientId,
                    Quantity = i.quantity
                }).ToList()
            };
        }

        public async Task<CartItemResponse> AddCartItemAsync(Guid userId, AddCartItemRequest request)
        {
            var cart = await _context.Cart
                .Include(c => c.Items)
                    .ThenInclude(i => i.Ingredients)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Domain.Entities.Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = new List<Domain.Entities.CartItem>()
                };
                _context.Cart.Add(cart);
            }

            var allIngredients = await _context.Ingredients.ToListAsync();
            var selectedIngredients = new List<Ingredients>();

            foreach (var idStr in request.IngredientIds)
            {
                if (Guid.TryParse(idStr, out var g))
                {
                    var found = allIngredients.FirstOrDefault(i => i.Id == g);
                    if (found != null && !selectedIngredients.Contains(found))
                    {
                        selectedIngredients.Add(found);
                    }
                }
                else if (int.TryParse(idStr, out var idx))
                {
                    if (idx > 0 && idx <= allIngredients.Count)
                    {
                        var found = allIngredients[idx - 1];
                        if (!selectedIngredients.Contains(found))
                        {
                            selectedIngredients.Add(found);
                        }
                    }
                }
            }

            string description = selectedIngredients.Any()
                ? string.Join(", ", selectedIngredients.Select(i => i.name))
                : "Custom pizza with selected ingredients";

            var cartItem = new Domain.Entities.CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                Size = request.Size,
                Price = request.UnitPrice,
                quantity = request.Quantity,
                Name = "Custom Pizza",
                Description = description,
                Image = "pizza.png",
                Ingredients = selectedIngredients.Select(i => new Domain.Entities.CartIngredient
                {
                    Id = Guid.NewGuid(),
                    IngredientId = i.Id,
                    quantity = 1
                }).ToList()
            };

            cart.Items.Add(cartItem);
            await _context.SaveChangesAsync();

            return new CartItemResponse
            {
                Id = cartItem.Id,
                CartItemId = cartItem.Id,
                PizzaId = cartItem.PizzaId,
                Name = cartItem.Name,
                Size = cartItem.Size,
                Description = cartItem.Description,
                Price = cartItem.Price,
                Quantity = cartItem.quantity,
                Image = cartItem.Image,
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
