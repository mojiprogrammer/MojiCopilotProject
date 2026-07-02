using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.AlarmEvent.Responses
{
    public sealed class AlarmHistoryResponse
    {
        public long AlarmEventId { get; set; }

        public string AlarmCode { get; set; } = string.Empty;

        public string AlarmName { get; set; } = string.Empty;

        public string ParameterName { get; set; } = string.Empty;

        public decimal Value { get; set; }

        public string Severity { get; set; } = string.Empty;

        public bool IsAcknowledged { get; set; }

        public DateTime EventTime { get; set; }
    }
}
