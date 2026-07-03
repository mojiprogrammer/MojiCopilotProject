namespace DanaCopilot.Contracts.ShiftSchedule.Requests
{
    public sealed class UpdateShiftScheduleRequest
    {
        public long Id { get; set; }

        public bool IsWorkingDay { get; set; }

        public bool IsActive { get; set; }

        public long ModifiedBy { get; set; }
    }
}
