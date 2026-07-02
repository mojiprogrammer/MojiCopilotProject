namespace DanaCopilot.Application.Modules.RunTime.Services
{
    public sealed class RuntimeHealthService
    {
        private DateTime _lastRun;

        public void MarkHeartbeat()
        {
            _lastRun = DateTime.UtcNow;
        }

        public bool IsHealthy()
        {
            return DateTime.UtcNow - _lastRun < TimeSpan.FromSeconds(5);
        }
    }
}
