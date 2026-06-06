using DanaCopilot.Domain;
using DanaCopilot.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly DanaAppDbContext _db;

        public ConversationRepository(DanaAppDbContext db)
        {
            _db = db;
        }

        public async Task<long> CreateAsync(Conversation conversation)
        {
            await _db.Conversations.AddAsync(conversation);
            await _db.SaveChangesAsync();
            return conversation.Id;
        }

        public async Task<Conversation?> GetByIdAsync(long id)
        {
            return await _db.Conversations.FindAsync(id);
        }

        public async Task UpdateAsync(Conversation conversation)
        {
            _db.Conversations.Update(conversation);
            await _db.SaveChangesAsync();
        }
    }
}
