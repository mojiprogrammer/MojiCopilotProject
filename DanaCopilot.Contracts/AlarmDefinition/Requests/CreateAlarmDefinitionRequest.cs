namespace DanaCopilot.Contracts.AlarmDefinition.Requests
{
    public sealed class CreateAlarmDefinitionRequest
    {
        public long ParameterId { get; set; }

        public string Code { get; set; } = "";

        public string Name { get; set; } = "";

        public string ConditionType { get; set; } = "";

        public decimal? ThresholdValue { get; set; }

        public decimal? MinValue { get; set; }

        public decimal? MaxValue { get; set; }

        public string Severity { get; set; } = "";

        public long CreatedBy { get; set; }
    }
}
