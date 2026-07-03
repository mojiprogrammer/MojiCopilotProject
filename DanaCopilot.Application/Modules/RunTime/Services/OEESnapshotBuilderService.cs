using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.OEESnapshot.Requests;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Application.Modules.RunTime.Services
{
    public sealed class OEESnapshotBuilderService : IOEESnapshotBuilderService
    {
        private readonly IOEESnapshotDataAccess _oeeRepository;
        private readonly IProductionExecutionDataAccess _executionRepository;
        private readonly IOEECalculationService _calculator;

        public OEESnapshotBuilderService(IOEESnapshotDataAccess oeeRepository, IProductionExecutionDataAccess executionRepository, IOEECalculationService calculator)
        {
            _oeeRepository = oeeRepository;
            _executionRepository = executionRepository;
            _calculator = calculator;
        }

        public async Task<long> BuildAsync(long productionLineId, DateOnly date)
        {
            var executions = await _executionRepository.GetByOrderAsync(productionLineId);

            var list = executions.ToList();

            if (!list.Any())
                return 0;

            var totalProduced = list.Sum(x => x.ProducedQuantity);
            var good = list.Sum(x => x.GoodQuantity);
            var reject = list.Sum(x => x.RejectQuantity);

            var plannedTime = list.Sum(x => x.PlannedQuantity); // simplified mapping
            var runTime = list.Sum(x => x.GoodQuantity);        // placeholder mapping
            var downtime = 0;

            var availability = _calculator.CalculateAvailability(runTime: (int)runTime, plannedTime: plannedTime > 0 ? (int)plannedTime : 1);

            var performance = _calculator.CalculatePerformance(idealCycleTime: 1, actualCycleTime: 1, totalCount: totalProduced);

            var quality = _calculator.CalculateQuality(good, totalProduced);

            var oee = _calculator.CalculateOEE(availability, performance, quality);

            var request = new CreateOEESnapshotRequest
            {
                ProductionLineId = productionLineId,
                ProductionDate = date,
                AvailableTimeMinutes = (int)plannedTime,
                PlannedProductionTimeMinutes = (int)plannedTime,
                RunTimeMinutes = (int)runTime,
                DowntimeMinutes = downtime,
                TotalProducedQuantity = totalProduced,
                GoodQuantity = good,
                RejectQuantity = reject
            };

            return await _oeeRepository.InsertAsync(request);
        }
    }
}
