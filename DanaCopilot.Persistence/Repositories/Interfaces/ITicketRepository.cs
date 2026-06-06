using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task CreateAsync(Ticket ticket);

        Task<List<Ticket>> SearchAsync(string query);
    }
}
