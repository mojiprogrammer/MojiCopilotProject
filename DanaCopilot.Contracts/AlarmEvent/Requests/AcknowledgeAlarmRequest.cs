using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.AlarmEvent.Requests
{
    public sealed class AcknowledgeAlarmRequest
    {
        public long AlarmEventId { get; set; }

        public long AcknowledgedBy { get; set; }
    }
}
