namespace DanaCopilot.Contracts.ReworkReason.Requests
{
    public sealed class DeleteReworkReasonRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
