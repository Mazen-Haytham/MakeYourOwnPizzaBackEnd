using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTimeOffset createdAt { get; set; }= DateTimeOffset.Now;

        public bool IsDeleted { get; set; }= false;
        public ICollection<CartItem> Items { get; set; } = new HashSet<CartItem>();
    }
}
