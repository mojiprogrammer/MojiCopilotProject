using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Documents
{
    public class DocumentDto
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public DocumentStatus Status { get; set; }

        public DateTime UploadedAt { get; set; }
    }
}
