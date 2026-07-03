using DanaCopilot.Contracts.ProductionExecution.Requests;
using DanaCopilot.Contracts.ProductionExecution.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{

    public sealed class ProductionExecutionDataAccess : BaseDataAccess, IProductionExecutionDataAccess
    {
        public ProductionExecutionDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<long> InsertAsync(CreateProductionExecutionRequest request)
        {
            return ExecuteScalarAsync<long>("runtime.sp_ProductionExecution_Insert", request);
        }

        public Task UpdateAsync(UpdateProductionExecutionRequest request)
        {
            return ExecuteAsync("runtime.sp_ProductionExecution_Update", request);
        }

        public Task CloseAsync(CloseProductionExecutionRequest request)
        {
            return ExecuteAsync("runtime.sp_ProductionExecution_Close", request);
        }

        public Task<ProductionExecutionResponse?> GetByIdAsync(long id)
        {
            return QueryFirstOrDefaultAsync<ProductionExecutionResponse>("runtime.sp_ProductionExecution_GetById",
                new
                {
                    Id = id
                });
        }

        public Task<IEnumerable<ProductionExecutionResponse>> GetByOrderAsync(long productionOrderId)
        {
            return QueryAsync<ProductionExecutionResponse>("runtime.sp_ProductionExecution_GetByOrder",
                new
                {
                    ProductionOrderId = productionOrderId
                });
        }

        public Task<IEnumerable<ProductionExecutionResponse>> GetByShiftAsync(long shiftId)
        {
            return QueryAsync<ProductionExecutionResponse>("runtime.sp_ProductionExecution_GetByShift",
                new
                {
                    ShiftId = shiftId
                });
        }

        public Task<ProductionExecutionSummaryResponse?> GetDailySummaryAsync(DateOnly productionDate)
        {
            return QueryFirstOrDefaultAsync<ProductionExecutionSummaryResponse>("runtime.sp_ProductionExecution_GetDailySummary",
                new
                {
                    ProductionDate = productionDate
                });
        }
    }
}
