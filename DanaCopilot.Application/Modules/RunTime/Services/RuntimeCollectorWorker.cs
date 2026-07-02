using Microsoft.Extensions.Hosting;

namespace DanaCopilot.Application.Modules.RunTime.Services
{
    public sealed class RuntimeCollectorWorker : BackgroundService
    {
        private readonly RuntimeCollectorService _service;

        public RuntimeCollectorWorker(RuntimeCollectorService service)
        {
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // simulate multiple PLCs
                await _service.CollectAsync(plcId: 1);
                await _service.CollectAsync(plcId: 2);

                await Task.Delay(1000, stoppingToken); // 1 sec cycle
            }
        }
    }
}
