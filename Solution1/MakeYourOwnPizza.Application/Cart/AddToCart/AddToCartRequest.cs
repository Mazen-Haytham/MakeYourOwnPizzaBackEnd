namespace MakeYourOwnPizza.Application.Cart.AddToCart
{
    public class AddToCartRequest
    {
        public Guid PizzaId { get; set; }
        public int Quantity { get; set; }
        public List<CartIngredientRequest> Ingredients { get; set; } = new();
    }
}