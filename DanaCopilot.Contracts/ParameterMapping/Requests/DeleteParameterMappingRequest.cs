namespace DanaCopilot.Contracts.ParameterMapping.Requests
{
    public sealed class DeleteParameterMappingRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
