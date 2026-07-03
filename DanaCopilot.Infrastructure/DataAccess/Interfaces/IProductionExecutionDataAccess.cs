using DanaCopilot.Contracts.ProductionExecution.Requests;
using DanaCopilot.Contracts.ProductionExecution.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{

    public interface IProductionExecutionDataAccess
    {
        Task<long> InsertAsync(CreateProductionExecutionRequest request);

        Task UpdateAsync(UpdateProductionExecutionRequest request);

        Task CloseAsync(CloseProductionExecutionRequest request);

        Task<ProductionExecutionResponse?> GetByIdAsync(long id);

        Task<IEnumerable<ProductionExecutionResponse>> GetByOrderAsync(long productionOrderId);

        Task<IEnumerable<ProductionExecutionResponse>> GetByShiftAsync(long shiftId);

        Task<ProductionExecutionSummaryResponse?> GetDailySummaryAsync(DateOnly productionDate);
    }
}
