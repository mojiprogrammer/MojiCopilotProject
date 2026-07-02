namespace DanaCopilot.Infrastructure.Models
{
    public sealed class ParameterValueInsertModel
    {
        public long ParameterId { get; set; }

        public long PLCId { get; set; }

        public long? StationId { get; set; }

        public decimal NumericValue { get; set; }

        public string Value { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }
    }
}
