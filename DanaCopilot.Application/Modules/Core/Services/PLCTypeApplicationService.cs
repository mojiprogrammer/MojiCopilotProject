using DanaCopilot.Application.Modules.Core.Interfaces;
using DanaCopilot.Contracts.PLCType.Requests;
using DanaCopilot.Contracts.PLCType.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Modules.Core.Services
{
    public sealed class PLCTypeApplicationService : IPLCTypeApplicationService
    {
        private readonly IPLCTypeDataAccess _dataAccess;

        public PLCTypeApplicationService(IPLCTypeDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<IEnumerable<PLCTypeResponse>> GetAllAsync() => _dataAccess.GetAllAsync();

        public Task<PLCTypeResponse?> GetByIdAsync(long id) => _dataAccess.GetByIdAsync(id);

        public Task<long> CreateAsync(CreatePLCTypeRequest request) => _dataAccess.InsertAsync(request);

        public Task UpdateAsync(UpdatePLCTypeRequest request) => _dataAccess.UpdateAsync(request);

        public Task DeleteAsync(DeletePLCTypeRequest request) => _dataAccess.DeleteAsync(request);
    }
}
