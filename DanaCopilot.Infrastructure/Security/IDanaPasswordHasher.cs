using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.Security
{
    public interface IDanaPasswordHasher
    {
        string Hash(string password);

        bool Verify(
            string password,
            string passwordHash);
    }
}
