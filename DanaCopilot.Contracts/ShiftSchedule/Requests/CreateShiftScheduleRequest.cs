namespace DanaCopilot.Contracts.ShiftSchedule.Requests
{
    public sealed class CreateShiftScheduleRequest
    {
        public long ShiftId { get; set; }

        public byte DayOfWeek { get; set; }

        public bool IsWorkingDay { get; set; }

        public long CreatedBy { get; set; }
    }
}
