using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Domain.Entities
{
    public class EmailVerification
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string OtpHash { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public bool IsUsed { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;
    }
}
