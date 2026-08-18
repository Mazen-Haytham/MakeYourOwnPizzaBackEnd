using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Domain.Entities
{
    public class Ingredients
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public decimal stock { get; set; }
        public decimal price { get; set; }
        public string imageUrl { get; set; }
        public ICollection<OrderIngredient> orderIngredients { get; set; } = new HashSet<OrderIngredient>();
    }
}
