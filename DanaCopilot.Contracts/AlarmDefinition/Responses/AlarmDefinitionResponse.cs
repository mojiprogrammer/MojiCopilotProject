namespace DanaCopilot.Contracts.AlarmDefinition.Responses
{
    public sealed class AlarmDefinitionResponse
    {
        public long Id { get; set; }

        public long ParameterId { get; set; }

        public string ParameterCode { get; set; } = "";

        public string ParameterName { get; set; } = "";

        public string Code { get; set; } = "";

        public string Name { get; set; } = "";

        public string ConditionType { get; set; } = "";

        public decimal? ThresholdValue { get; set; }

        public decimal? MinValue { get; set; }

        public decimal? MaxValue { get; set; }

        public string Severity { get; set; } = "";

        public bool IsActive { get; set; }
    }
}
