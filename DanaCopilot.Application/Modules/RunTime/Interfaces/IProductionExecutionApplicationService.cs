using DanaCopilot.Contracts.ProductionExecution.Requests;
using DanaCopilot.Contracts.ProductionExecution.Responses;

namespace DanaCopilot.Application.Modules.RunTime.Interfaces
{

    public interface IProductionExecutionApplicationService
    {
        Task<long> CreateAsync(CreateProductionExecutionRequest request);

        Task UpdateAsync(UpdateProductionExecutionRequest request);

        Task CloseAsync(CloseProductionExecutionRequest request);

        Task<ProductionExecutionResponse?> GetByIdAsync(long id);

        Task<IEnumerable<ProductionExecutionResponse>> GetByOrderAsync(long productionOrderId);

        Task<IEnumerable<ProductionExecutionResponse>> GetByShiftAsync(long shiftId);

        Task<ProductionExecutionSummaryResponse?> GetDailySummaryAsync(DateOnly productionDate);
    }
}
