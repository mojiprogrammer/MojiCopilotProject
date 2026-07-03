namespace DanaCopilot.Contracts.ShiftSchedule.Requests
{
    public sealed class DeleteShiftScheduleRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
