using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Domain.Entities
{
    public class OrderIngredient
    {
        public Guid Id { get; set; }
        public Guid orderItemId { get; set; }
        public OrderItem orderItem { get; set; }
        public Guid ingredientId { get; set; }
        public Ingredients Ingredient { get; set; }
        public decimal quantity { get; set; }
    }
}
