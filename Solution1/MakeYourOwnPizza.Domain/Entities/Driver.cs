using System;
using System.Collections.Generic;

namespace MakeYourOwnPizza.Domain.Entities
{
    public class Driver
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }

        public string Zone { get; set; } = string.Empty;
        public Enums.DriverStatus Status { get; set; } = Enums.DriverStatus.Offline;

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
