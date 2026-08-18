using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid orderId { get; set; }
        public Order order { get; set; }
        public Guid pizzaId { get; set; }
        public Pizza pizza { get; set; }
        public decimal quantity { get; set; }
        public decimal price { get; set; }
        public ICollection<OrderIngredient> orderIngredients { get; set; } = new HashSet<OrderIngredient>();


    }
}
