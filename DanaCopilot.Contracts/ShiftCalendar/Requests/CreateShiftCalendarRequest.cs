namespace DanaCopilot.Contracts.ShiftCalendar.Requests
{
    public sealed class CreateShiftCalendarRequest
    {
        public long ShiftId { get; set; }

        public DateOnly ProductionDate { get; set; }

        public bool IsHoliday { get; set; }

        public bool IsWorkingDay { get; set; }

        public string? Notes { get; set; }

        public long CreatedBy { get; set; }
    }
}
