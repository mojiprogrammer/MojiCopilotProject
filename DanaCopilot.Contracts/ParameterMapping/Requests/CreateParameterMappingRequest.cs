namespace DanaCopilot.Contracts.ParameterMapping.Requests
{
    public sealed class CreateParameterMappingRequest
    {
        public long ParameterId { get; set; }

        public long PLCId { get; set; }

        public string SignalAddress { get; set; } = string.Empty;

        public string? SignalName { get; set; }

        public decimal ScaleFactor { get; set; } = 1;

        public decimal OffsetValue { get; set; } = 0;

        public string? Formula { get; set; }

        public long CreatedBy { get; set; }
    }
}
