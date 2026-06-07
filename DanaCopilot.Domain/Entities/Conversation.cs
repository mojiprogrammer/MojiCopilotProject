using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain
{
    public class Conversation
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActivityAt { get; set; } = DateTime.MinValue;

        public long OrganizationId { get; set; }
        public string Title { get; set; }
        public bool IsArchived { get; set; }
    }
}
