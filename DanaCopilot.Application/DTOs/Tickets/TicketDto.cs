using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Tickets
{
    public class TicketDto
    {
        public long Id { get; set; }

        public string TicketNumber { get; set; }

        public string ProductModel { get; set; }

        public string Subject { get; set; }

        public bool IsResolved { get; set; }
    }
}
