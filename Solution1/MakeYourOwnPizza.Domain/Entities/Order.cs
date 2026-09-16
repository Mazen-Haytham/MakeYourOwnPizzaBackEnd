using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeYourOwnPizza.Domain.Enums;

namespace MakeYourOwnPizza.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid userId { get; set; }
        public User user { get; set; }
        public Guid? driverId { get; set; }
        public Driver driver { get; set; }
        public DateTimeOffset createdAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset estimatedDelivery { get; set; }
        public decimal totalPrice { get; set; }
        public PaymentMethod paymentMethod { get; set; }

        public string? note { get; set; }

        // Delivery Address
        public string? street { get; set; }
        public string? district { get; set; }
        public string? city { get; set; }
        public string? floor { get; set; }
        public string? apartment { get; set; }
        public string? formattedAddress { get; set; }

        public bool isActive { get; set; } = true;

        public ICollection<OrderItem> orderItems { get; set; } = new HashSet<OrderItem>();
        public ICollection<Payment> payments { get; set; } = new HashSet<Payment>();
        public ICollection<OrderStage> orderStages { get; set; } = new HashSet<OrderStage>();
    }
}
