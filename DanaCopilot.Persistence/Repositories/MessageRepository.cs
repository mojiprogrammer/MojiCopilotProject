using DanaCopilot.Domain;
using DanaCopilot.Persistence.Repositories.Interfaces;
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

        public async Task<Message?> GetByIdAsync(
            long id)
        {
            return await _db.Messages
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Message>> GetByConversationIdAsync(long conversationId)
        {
            return await _db.Messages
                .AsNoTracking()
                .Where(x =>
                    x.ConversationId == conversationId)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task UpdateAsync(Message message)
        {
            _db.Messages.Update(message);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(
            long id)
        {
            var entity = await _db.Messages
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return;

            _db.Messages.Remove(entity);

            await _db.SaveChangesAsync();
        }
    }
}
