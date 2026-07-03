namespace DanaCopilot.Contracts.ShiftCalendar.Requests
{
    public sealed class DeleteShiftCalendarRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
