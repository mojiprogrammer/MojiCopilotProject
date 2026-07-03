namespace DanaCopilot.Contracts.ShiftSchedule.Responses
{
    public sealed class ShiftScheduleResponse
    {
        public long Id { get; set; }

        public long ShiftId { get; set; }

        public byte DayOfWeek { get; set; }

        public bool IsWorkingDay { get; set; }

        public bool IsActive { get; set; }
    }
}
