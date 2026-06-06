using DanaCopilot.Application.DTOs.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application
{
    public interface ITicketService
    {
        Task<long> CreateAsync(
            TicketDto dto);

        Task<List<TicketDto>> SearchAsync(
            string query);
    }
}
