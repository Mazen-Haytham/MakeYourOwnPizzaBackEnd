namespace MakeYourOwnPizza.Application.Cart.GetCart
{
    public class GetCartResponse
    {
        public Guid CartId { get; set; }
        public List<CartItemResponse> Items { get; set; } = new();
    }
}