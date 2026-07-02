namespace DanaCopilot.Contracts.Parameter.Requests
{
    public sealed class CreateParameterRequest
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string DataType { get; set; } = string.Empty;

        public string? Unit { get; set; }

        public string? MinValue { get; set; }

        public string? MaxValue { get; set; }

        public bool IsKPI { get; set; }

        public long CreatedBy { get; set; }
    }
}
