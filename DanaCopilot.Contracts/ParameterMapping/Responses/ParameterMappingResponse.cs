namespace DanaCopilot.Contracts.ParameterMapping.Responses
{
    public sealed class ParameterMappingResponse
    {
        public long Id { get; set; }

        public long ParameterId { get; set; }

        public string ParameterCode { get; set; } = string.Empty;

        public string ParameterName { get; set; } = string.Empty;

        public long PLCId { get; set; }

        public string PLCName { get; set; } = string.Empty;

        public string SignalAddress { get; set; } = string.Empty;

        public string? SignalName { get; set; }

        public decimal ScaleFactor { get; set; }

        public decimal OffsetValue { get; set; }

        public string? Formula { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
