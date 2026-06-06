using DanaCopilot.Domain;
using DanaCopilot.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DanaCopilot.Persistence.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DanaAppDbContext _db;

        public MessageRepository(DanaAppDbContext db)
        {
            _db = db;
        }

        public async Task<long> CreateAsync(Message message)
        {
            await _db.Messages.AddAsync(message);
            await _db.SaveChangesAsync();
            return message.Id;
        }

        public async Task<List<Message>> GetByConversationIdAsync(long conversationId)
        {
            return await _db.Messages
                .Where(x => x.ConversationId == conversationId)
                .ToListAsync();
        }
    }
}
