using MakeYourOwnPizza.Application.Cart;

namespace MakeYourOwnPizza.Application.Cart.GetCart
{
    public class CartItemResponse
    {
        public Guid CartItemId { get; set; }
        public Guid PizzaId { get; set; }
        public int Quantity { get; set; }

        public List<CartIngredientResponse> Ingredients { get; set; } = new();
    }
}