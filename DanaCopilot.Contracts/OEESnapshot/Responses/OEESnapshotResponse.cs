namespace DanaCopilot.Contracts.OEESnapshot.Responses
{
    public sealed class OEESnapshotResponse
    {
        public long Id { get; set; }

        public long ProductionLineId { get; set; }

        public long? ShiftId { get; set; }

        public DateOnly ProductionDate { get; set; }

        public decimal Availability { get; set; }

        public decimal Performance { get; set; }

        public decimal Quality { get; set; }

        public decimal OEE { get; set; }
    }
}
