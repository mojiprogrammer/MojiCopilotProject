using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.Shift.Requests;
using DanaCopilot.Contracts.Shift.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Application.Modules.RunTime.Services
{

    public sealed class ShiftApplicationService : IShiftApplicationService
    {
        private readonly IShiftDataAccess _repository;

        public ShiftApplicationService(IShiftDataAccess repository)
        {
            _repository = repository;
        }

        public Task<long> CreateAsync(CreateShiftRequest request) => _repository.InsertAsync(request);

        public Task UpdateAsync(UpdateShiftRequest request) => _repository.UpdateAsync(request);

        public Task DeleteAsync(DeleteShiftRequest request) => _repository.DeleteAsync(request);

        public Task<IEnumerable<ShiftResponse>> GetAllAsync() => _repository.GetAllAsync();

        public Task<ShiftResponse?> GetByIdAsync(long id) => _repository.GetByIdAsync(id);
    }
}
