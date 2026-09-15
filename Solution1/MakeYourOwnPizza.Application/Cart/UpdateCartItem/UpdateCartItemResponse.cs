
namespace MakeYourOwnPizza.Application.Cart.UpdateCartItem
{
    public     class UpdateCartItemResponse
    {
        public Guid CartId { get; set; }
        public Guid CartItemId { get; set; }
        public int Quantity { get; set; }

        public List<CartIngredientResponse> Ingredients { get; set; } = new();
    }
}
