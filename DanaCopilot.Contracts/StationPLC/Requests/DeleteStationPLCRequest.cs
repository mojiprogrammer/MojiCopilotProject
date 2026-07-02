namespace DanaCopilot.Contracts.StationPLC.Requests
{
    public sealed class DeleteStationPLCRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
