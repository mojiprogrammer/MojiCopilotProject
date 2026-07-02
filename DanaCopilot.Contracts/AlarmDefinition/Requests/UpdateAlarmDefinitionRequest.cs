using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.AlarmDefinition.Requests
{
    public sealed class UpdateAlarmDefinitionRequest
    {
        public long Id { get; set; }

        public string Name { get; set; } = "";

        public string ConditionType { get; set; } = "";

        public decimal? ThresholdValue { get; set; }

        public decimal? MinValue { get; set; }

        public decimal? MaxValue { get; set; }

        public string Severity { get; set; } = "";

        public long ModifiedBy { get; set; }
    }
}
