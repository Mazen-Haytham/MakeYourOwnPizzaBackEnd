using System;
using MakeYourOwnPizza.Domain.Enums;

namespace MakeYourOwnPizza.Application.Users
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public Role role { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
}
