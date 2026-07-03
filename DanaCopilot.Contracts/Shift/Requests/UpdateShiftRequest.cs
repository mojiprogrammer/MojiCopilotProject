namespace DanaCopilot.Contracts.Shift.Requests
{
    public sealed class UpdateShiftRequest
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int DurationInMinutes { get; set; }

        public bool IsNightShift { get; set; }

        public bool IsActive { get; set; }

        public long ModifiedBy { get; set; }
    }
}
