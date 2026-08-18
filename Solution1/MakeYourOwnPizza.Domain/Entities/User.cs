using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeYourOwnPizza.Domain.Enums;

namespace MakeYourOwnPizza.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;

        public Role role { get; set; }
        public string? refreshToken { get; set; } = null;
        public DateTime? refreshTokenExpiryTime { get; set; } = null;
        public bool isActive { get; set; } = false;
        public bool isDeleted { get; set; } = false;
        public ICollection<Order> orders { get; set; } = new HashSet<Order>();
        public ICollection<EmailVerification> EmailVerifications { get; set; } = new HashSet<EmailVerification>();
    }
}
