using MakeYourOwnPizza.Application.Cart.AddToCart;
namespace MakeYourOwnPizza.Application.Cart.UpdateCartItem
{
    public class UpdateCartItemRequest
    {
        public int Quantity { get; set; }
        public List<CartIngredientRequest> Ingredients { get; set; } = new();
    }
}
