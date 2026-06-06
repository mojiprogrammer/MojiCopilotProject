using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain
{
    public class Ticket
    {
        public long Id { get; set; }

        public long OrganizationId { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string Solution { get; set; }
    }
}
