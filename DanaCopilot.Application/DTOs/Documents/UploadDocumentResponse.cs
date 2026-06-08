using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Documents
{
    public class UploadDocumentResponse
    {
        public long DocumentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DocumentStatus Status { get; set; }
    }
}
