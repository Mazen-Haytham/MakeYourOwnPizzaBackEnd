using MakeYourOwnPizza.Application.Cart;

namespace MakeYourOwnPizza.Application.Cart.AddToCart
{
    public class AddToCartResponse
    {
        public Guid CartItemId { get; set; }
        public Guid PizzaId { get; set; }
        public int Quantity { get; set; }

        public List<CartIngredientResponse> Ingredients { get; set; } = new();
    }
}