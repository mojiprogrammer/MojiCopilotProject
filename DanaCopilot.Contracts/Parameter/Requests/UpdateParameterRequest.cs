namespace DanaCopilot.Contracts.Parameter.Requests
{
    public sealed class UpdateParameterRequest
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string DataType { get; set; } = string.Empty;

        public string? Unit { get; set; }

        public string? MinValue { get; set; }

        public string? MaxValue { get; set; }

        public bool IsKPI { get; set; }

        public long ModifiedBy { get; set; }
    }
}
