using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain
{
    public class Conversation
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long OrganizationId { get; set; }
    }
}
