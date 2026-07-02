using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;

namespace DanaCopilot.Application.Modules.RunTime.Services
{
    public sealed class RuntimeEngineWorker : BackgroundService
    {
        private readonly RuntimeProducerService _producer;
        private readonly RuntimeConsumerService _consumer;

        public RuntimeEngineWorker(RuntimeProducerService producer, RuntimeConsumerService consumer)
        {
            _producer = producer;
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerTask = _consumer.StartAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await _producer.ReadPlcAsync(plcId: 1);
                await _producer.ReadPlcAsync(plcId: 2);

                await Task.Delay(1000, stoppingToken);
            }

            await consumerTask;
        }
    }
}
