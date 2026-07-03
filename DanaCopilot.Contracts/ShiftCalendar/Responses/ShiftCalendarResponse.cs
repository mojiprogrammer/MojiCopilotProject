namespace DanaCopilot.Contracts.ShiftCalendar.Responses
{
    public sealed class ShiftCalendarResponse
    {
        public long Id { get; set; }

        public long ShiftId { get; set; }

        public DateOnly ProductionDate { get; set; }

        public bool IsHoliday { get; set; }

        public bool IsWorkingDay { get; set; }

        public string? Notes { get; set; }
    }
}
