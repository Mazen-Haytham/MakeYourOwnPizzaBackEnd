using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Domain.Entities
{
    public class CartIngredient
    {
        public Guid Id { get; set; }
        public Guid CartItemId { get; set; }
        public CartItem CartItem {  get; set; }
        public Guid IngredientId { get; set; }
        public Ingredients Ingredients { get; set; }
        public int quantity { get; set; }
    }
}
