using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.Line.Requests
{
    public sealed class DeleteLineRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
