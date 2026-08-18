using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid orderId { get; set; }
        public Order order { get; set; }
        public decimal amount { get; set; }

        public string transactionId { get; set; }
        public DateTimeOffset paymentDate { get; set; }
        public string idempotentKey { get; set; }
    }
}
