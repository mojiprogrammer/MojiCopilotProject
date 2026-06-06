using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Tickets
{
    public class SearchTicketRequest
    {
        public string Query { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
