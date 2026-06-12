using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain
{
    public class User
    {
        public long Id { get; set; }

        public int OrganizationId { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public UserRole Role { get; set; }
    }
}
