using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeYourOwnPizza.Domain.Enums;
namespace MakeYourOwnPizza.Application.Abstractions.Persistence
{
    public class AddIngredientDTO
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string colorHex { get; set; }
        public bool isAvailable { get; set; } = true;
        public IngredientCategory category { get; set; }
    }
    public class GetIngredientDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string colorHex { get; set; }
        public bool isAvailable { get; set; } = true;
        public IngredientCategory category { get; set; }
    }
}
