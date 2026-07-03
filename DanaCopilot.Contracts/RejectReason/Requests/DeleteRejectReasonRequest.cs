using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.RejectReason.Requests
{
    public sealed class DeleteRejectReasonRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
