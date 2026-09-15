namespace MakeYourOwnPizza.Application.Cart.AddToCart
{
    public class CartIngredientRequest
    {
        public Guid IngredientId { get; set; }
        public int Quantity { get; set; }
    }
}