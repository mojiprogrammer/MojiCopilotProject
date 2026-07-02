using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.AlarmEvent.Responses
{
    public sealed class AlarmEventResponse
    {
        public long Id { get; set; }

        public long AlarmDefinitionId { get; set; }

        public long ParameterId { get; set; }

        public long PLCId { get; set; }

        public long? StationId { get; set; }

        public decimal Value { get; set; }

        public string Severity { get; set; } = string.Empty;

        public string? Message { get; set; }

        public bool IsAcknowledged { get; set; }

        public DateTime EventTime { get; set; }
    }
}
