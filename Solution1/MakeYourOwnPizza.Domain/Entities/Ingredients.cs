using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeYourOwnPizza.Domain.Enums;

namespace MakeYourOwnPizza.Domain.Entities
{
    public class Ingredients
    {
        public Guid Id { get; set; }
        public string name { get; set; }=string.Empty;
        public string colorHex { get; set; } = string.Empty;
        public decimal stock { get; set; }
        public decimal price { get; set; }
        public bool isAvailable { get; set; } = true;
        public IngredientCategory category { get; set; }
        public string imageUrl { get; set; } = string.Empty;
        public ICollection<OrderIngredient> orderIngredients { get; set; } = new HashSet<OrderIngredient>();
    }
}
