namespace DanaCopilot.Contracts.Shift.Responses
{
    public sealed class ShiftResponse
    {
        public long Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int DurationInMinutes { get; set; }

        public bool IsNightShift { get; set; }

        public bool IsActive { get; set; }
    }
}
