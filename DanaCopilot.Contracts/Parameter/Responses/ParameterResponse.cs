namespace DanaCopilot.Contracts.Parameter.Responses
{
    public sealed class ParameterResponse
    {
        public long Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string DataType { get; set; } = string.Empty;

        public string? Unit { get; set; }

        public string? MinValue { get; set; }

        public string? MaxValue { get; set; }

        public bool IsKPI { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
