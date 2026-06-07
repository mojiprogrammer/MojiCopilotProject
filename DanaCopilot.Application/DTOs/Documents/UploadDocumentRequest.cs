using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Documents
{
    public class UploadDocumentRequest
    {
        public long UserId { get; set; }

        public string Title { get; set; }        

        public Stream FileStream { get; set; }

        public string FileName { get; set; }
    }
}
