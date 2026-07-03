namespace DanaCopilot.Contracts.Shift.Requests
{
    public sealed class DeleteShiftRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
