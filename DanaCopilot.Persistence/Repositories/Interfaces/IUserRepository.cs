using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(long id);

        Task<User?> GetByUsernameAsync(string username);

        Task CreateAsync(User user);

        Task UpdateAsync(User user);
    }
}
