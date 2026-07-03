namespace DanaCopilot.Contracts.OEESnapshot.Requests
{
    public sealed class CreateOEESnapshotRequest
    {
        public long ProductionLineId { get; set; }

        public long? ShiftId { get; set; }

        public DateOnly ProductionDate { get; set; }

        public int AvailableTimeMinutes { get; set; }

        public int PlannedProductionTimeMinutes { get; set; }

        public int RunTimeMinutes { get; set; }

        public int DowntimeMinutes { get; set; }

        public decimal TotalProducedQuantity { get; set; }

        public decimal GoodQuantity { get; set; }

        public decimal RejectQuantity { get; set; }
    }
}
