namespace DanaCopilot.Application.Modules.RunTime.Models
{
    public sealed class RuntimeDataItem
    {
        public long ParameterId { get; set; }

        public long PLCId { get; set; }

        public long? StationId { get; set; }

        public decimal Value { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
