using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Retrieval.Models
{
    public class VectorSearchRequest
    {
        public string Query { get; set; }

        public int TopK { get; set; } = 10;
    }
}
