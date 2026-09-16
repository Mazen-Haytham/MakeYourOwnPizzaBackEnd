using System.Collections.Generic;

namespace MakeYourOwnPizza.Application.Cart.AddCartItem
{
    public class AddCartItemRequest
    {
        public string Size { get; set; } = string.Empty;
        public List<string> IngredientIds { get; set; } = new();
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
