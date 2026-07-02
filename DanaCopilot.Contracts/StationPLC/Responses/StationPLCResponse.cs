namespace DanaCopilot.Contracts.StationPLC.Responses
{
    public sealed class StationPLCResponse
    {
        public long Id { get; set; }

        public long StationId { get; set; }

        public string StationName { get; set; } = string.Empty;

        public long PLCId { get; set; }

        public string PLCCode { get; set; } = string.Empty;

        public string PLCName { get; set; } = string.Empty;

        public bool IsPrimary { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
