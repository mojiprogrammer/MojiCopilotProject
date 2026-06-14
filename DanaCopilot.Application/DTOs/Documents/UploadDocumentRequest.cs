using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Documents
{
    public class UploadDocumentRequest
    {
        public int? UserId { get; set; }
        public int ConversationId  { get; set; }

        public string Title { get; set; }        

        public Stream FileStream { get; set; }

        public string FileName { get; set; }


    }
}
