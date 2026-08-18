using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Domain.Entities
{
    public class OrderStage
    {
        public Guid Id { get; set; }
        public Guid orderId { get; set; }
        public Order order { get; set; }
        public string stageType { get; set; }
        public DateTimeOffset createdAt { get; set; } = DateTimeOffset.Now;
    }
}
