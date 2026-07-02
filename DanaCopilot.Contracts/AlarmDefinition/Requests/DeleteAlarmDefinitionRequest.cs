namespace DanaCopilot.Contracts.AlarmDefinition.Requests
{
    public sealed class DeleteAlarmDefinitionRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
