namespace DanaCopilot.Contracts.ReworkReason.Requests
{
    public sealed class UpdateReworkReasonRequest
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public long ModifiedBy { get; set; }
    }
}
