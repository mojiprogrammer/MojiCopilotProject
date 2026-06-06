using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain.Entities
{
    public class Document
    {
        public long Id { get; set; }

        public long OrganizationId { get; set; }

        public string Title { get; set; }

        public string FilePath { get; set; }

        public DocumentStatus Status { get; set; }
    }
}
