using DanaCopilot.Application.Modules.RunTime.Models;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;
using DanaCopilot.Infrastructure.Models;

namespace DanaCopilot.Application.Modules.RunTime.Services
{
     public sealed class RuntimeConsumerService
    {
        private readonly IParameterValueDataAccess _repo;

        public RuntimeConsumerService(IParameterValueDataAccess repo)
        {
            _repo = repo;
        }

        public async Task StartAsync(CancellationToken token)
        {
            var batch = new List<RuntimeDataItem>();

            await foreach (var item in RuntimeChannel.Channel.Reader.ReadAllAsync(token))
            {
                batch.Add(item);

                if (batch.Count >= 50)
                {
                    await Flush(batch);
                    batch.Clear();
                }
            }
        }

        private Task Flush(List<RuntimeDataItem> batch)
        {
            return _repo.BulkInsertAsync(
                batch.Select(x => new ParameterValueInsertModel
                {
                    ParameterId = x.ParameterId,
                    PLCId = x.PLCId,
                    StationId = x.StationId,
                    NumericValue = x.Value,
                    Value = x.Value.ToString(),
                    Timestamp = x.Timestamp
                }));
        }
    }
}
