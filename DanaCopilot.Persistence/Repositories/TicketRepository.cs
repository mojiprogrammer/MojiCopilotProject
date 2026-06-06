using DanaCopilot.Domain;
using DanaCopilot.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DanaCopilot.Persistence
{
    public class TicketRepository
     : ITicketRepository
    {
        private readonly DanaAppDbContext _db;

        public TicketRepository(DanaAppDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(
            Ticket ticket)
        {
            await _db.Tickets.AddAsync(ticket);

            await _db.SaveChangesAsync();
        }

        public async Task<List<Ticket>> SearchAsync(
            string query)
        {
            query = query.Trim();

            return await _db.Tickets
                .Where(x =>
                    x.Subject.Contains(query) ||
                    x.Description.Contains(query) ||
                    x.ProductModel.Contains(query))
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
