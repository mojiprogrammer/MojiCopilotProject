using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.RejectReason.Requests;
using DanaCopilot.Contracts.RejectReason.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Modules.RunTime.Services
{
    public sealed class RejectReasonApplicationService : IRejectReasonApplicationService
    {
        private readonly IRejectReasonDataAccess _repository;

        public RejectReasonApplicationService(IRejectReasonDataAccess repository)
        {
            _repository = repository;
        }

        public Task<long> CreateAsync(CreateRejectReasonRequest request) => _repository.InsertAsync(request);

        public Task UpdateAsync(UpdateRejectReasonRequest request) => _repository.UpdateAsync(request);

        public Task DeleteAsync(DeleteRejectReasonRequest request) => _repository.DeleteAsync(request);

        public Task<IEnumerable<RejectReasonResponse>> GetAllAsync() => _repository.GetAllAsync();

        public Task<RejectReasonResponse?> GetByIdAsync(long id) => _repository.GetByIdAsync(id);
    }
}
