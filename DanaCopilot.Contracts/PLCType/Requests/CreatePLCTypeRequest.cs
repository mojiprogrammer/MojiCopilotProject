namespace DanaCopilot.Contracts.PLCType.Requests
{
    public sealed class CreatePLCTypeRequest
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public long CreatedBy { get; set; }
    }
}
