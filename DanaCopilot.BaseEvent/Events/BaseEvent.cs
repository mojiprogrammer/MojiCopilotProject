namespace DanaCopilot.EventMessages.Events
{
    public class BaseEvent
    {
        public BaseEvent()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.UtcNow;

        }
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
