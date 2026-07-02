using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.AlarmEvent.Responses
{
    public sealed class AlarmStatisticsResponse
    {
        public string Severity { get; set; } = string.Empty;

        public int Count { get; set; }
    }
}
