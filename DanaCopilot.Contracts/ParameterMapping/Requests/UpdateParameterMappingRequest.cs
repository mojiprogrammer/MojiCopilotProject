namespace DanaCopilot.Contracts.ParameterMapping.Requests
{
    public sealed class UpdateParameterMappingRequest
    {
        public long Id { get; set; }

        public decimal ScaleFactor { get; set; }

        public decimal OffsetValue { get; set; }

        public string? Formula { get; set; }

        public long ModifiedBy { get; set; }
    }
}
