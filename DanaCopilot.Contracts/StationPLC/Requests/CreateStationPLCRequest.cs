namespace DanaCopilot.Contracts.StationPLC.Requests
{
    public sealed class CreateStationPLCRequest
    {
        public long StationId { get; set; }

        public long PLCId { get; set; }

        public bool IsPrimary { get; set; }

        public long CreatedBy { get; set; }
    }
}
