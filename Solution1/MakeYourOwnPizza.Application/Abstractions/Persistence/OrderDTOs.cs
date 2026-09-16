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
        public string OrderId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public string Note { get; set; } = string.Empty;
        public int PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }

        public CustomerDto Customer { get; set; } = new CustomerDto();
        public DeliveryAddressDto DeliveryAddress { get; set; } = new DeliveryAddressDto();
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }

    public class CustomerDto
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class DeliveryAddressDto
    {
        public string Street { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Floor { get; set; } = string.Empty;
        public string Apartment { get; set; } = string.Empty;
        public string Formatted { get; set; } = string.Empty;
    }

    public class OrderItemDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<string> Toppings { get; set; } = new List<string>();
    }

    public class UpdateOrderStatusRequest
    {
        public string Status { get; set; } = string.Empty;
    }

    public class CheckoutOrderRequest
    {
        public List<CheckoutOrderItemRequest> Items { get; set; } = new();
        public int PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CheckoutOrderItemRequest
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
