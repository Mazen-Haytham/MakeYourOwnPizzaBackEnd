using System;
using System.Collections.Generic;
using MakeYourOwnPizza.Domain.Enums;

namespace MakeYourOwnPizza.Application.Abstractions.Persistence
{
    public class GetOrderResponse
    {
        public Guid OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PizzaCount { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class GetOrderDetailsResponse
    {
        public Guid OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string CustomerPhone { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public List<GetPizzaDto> Pizzas { get; set; } = new List<GetPizzaDto>();
    }

    public class GetPizzaDto
    {
        public Guid PizzaId { get; set; }
        public string PizzaName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<GetOrderIngredientDto> Ingredients { get; set; } = new List<GetOrderIngredientDto>();
    }

    public class GetOrderIngredientDto
    {
        public Guid IngredientId { get; set; }
        public string IngredientName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
    }
}
