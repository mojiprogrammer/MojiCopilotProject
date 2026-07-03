using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.ShiftSchedule.Requests;
using DanaCopilot.Contracts.ShiftSchedule.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Application.Modules.RunTime.Services
{

    public sealed class ShiftScheduleApplicationService : IShiftScheduleApplicationService
    {
        private readonly IShiftScheduleDataAccess _repository;

        public ShiftScheduleApplicationService(IShiftScheduleDataAccess repository)
        {
            _repository = repository;
        }

        public Task<long> CreateAsync(CreateShiftScheduleRequest request) => _repository.InsertAsync(request);

        public Task UpdateAsync(UpdateShiftScheduleRequest request) => _repository.UpdateAsync(request);

        public Task DeleteAsync(DeleteShiftScheduleRequest request) => _repository.DeleteAsync(request);

        public Task<IEnumerable<ShiftScheduleResponse>> GetAllAsync() => _repository.GetAllAsync();

        public Task<ShiftScheduleResponse?> GetByIdAsync(long id) => _repository.GetByIdAsync(id);

        public Task<IEnumerable<ShiftScheduleResponse>> GetByShiftIdAsync(long shiftId) => _repository.GetByShiftIdAsync(shiftId);
    }
}
