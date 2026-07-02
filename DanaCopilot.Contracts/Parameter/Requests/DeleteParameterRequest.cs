namespace DanaCopilot.Contracts.Parameter.Requests
{
    public sealed class DeleteParameterRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
