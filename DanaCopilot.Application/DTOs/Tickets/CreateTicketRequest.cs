using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Tickets
{
    public class CreateTicketRequest
    {
        public string ProductModel { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }
    }
}
