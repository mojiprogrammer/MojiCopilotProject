using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.ProductionExecution.Requests;
using DanaCopilot.Contracts.ProductionExecution.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Modules.RunTime.Services
{

    public sealed class ProductionExecutionApplicationService : IProductionExecutionApplicationService
    {
        private readonly IProductionExecutionDataAccess _repository;

        public ProductionExecutionApplicationService(IProductionExecutionDataAccess repository)
        {
            _repository = repository;
        }

        public Task<long> CreateAsync(CreateProductionExecutionRequest request) => _repository.InsertAsync(request);

        public Task UpdateAsync(UpdateProductionExecutionRequest request) => _repository.UpdateAsync(request);

        public Task CloseAsync(CloseProductionExecutionRequest request) => _repository.CloseAsync(request);

        public Task<ProductionExecutionResponse?> GetByIdAsync(long id) => _repository.GetByIdAsync(id);

        public Task<IEnumerable<ProductionExecutionResponse>> GetByOrderAsync(long productionOrderId) => _repository.GetByOrderAsync(productionOrderId);

        public Task<IEnumerable<ProductionExecutionResponse>> GetByShiftAsync(long shiftId) => _repository.GetByShiftAsync(shiftId);

        public Task<ProductionExecutionSummaryResponse?> GetDailySummaryAsync(DateOnly productionDate) => _repository.GetDailySummaryAsync(productionDate);
    }
}
