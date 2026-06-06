using DanaCopilot.Domain;
using DanaCopilot.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DanaCopilot.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly DanaAppDbContext _db;

        public UserRepository(DanaAppDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await _db.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _db.Users
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task CreateAsync(User user)
        {
            await _db.Users.AddAsync(user);

            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _db.Users.Update(user);

            await _db.SaveChangesAsync();
        }
    }
}
