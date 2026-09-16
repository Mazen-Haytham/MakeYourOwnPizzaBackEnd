using System;
using System.Collections.Generic;

namespace MakeYourOwnPizza.Application.Cart.GetCart
{
    public class CartItemResponse
    {
        public Guid Id { get; set; }
        public Guid CartItemId { get => Id; set => Id = value; }
        public Guid? PizzaId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; } = string.Empty;

        public List<CartIngredientResponse> Ingredients { get; set; } = new();
    }
}