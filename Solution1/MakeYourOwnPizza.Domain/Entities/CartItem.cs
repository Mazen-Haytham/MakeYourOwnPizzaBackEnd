using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        public Guid? PizzaId { get; set; }
        public Pizza? Pizza { get; set; }
        public int quantity { get; set; }
        public string? Size { get; set; }
        public decimal Price { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public ICollection<CartIngredient> Ingredients { get; set; }=new HashSet<CartIngredient>();
    }
}
