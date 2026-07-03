namespace DanaCopilot.Contracts.Shift.Requests
{
    public sealed class CreateShiftRequest
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int DurationInMinutes { get; set; }

        public bool IsNightShift { get; set; }

        public long CreatedBy { get; set; }
    }
}
