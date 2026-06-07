using DanaCopilot.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.Services
{
    
    public class DanaPasswordHasher : IDanaPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(
            string password,
            string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(
                password,
                passwordHash);
        }
    }
}
