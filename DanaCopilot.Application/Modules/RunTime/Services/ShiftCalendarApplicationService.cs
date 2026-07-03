using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.ShiftCalendar.Requests;
using DanaCopilot.Contracts.ShiftCalendar.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Application.Modules.RunTime.Services
{

    public sealed class ShiftCalendarApplicationService : IShiftCalendarApplicationService
    {
        private readonly IShiftCalendarDataAccess _repository;

        public ShiftCalendarApplicationService(IShiftCalendarDataAccess repository)
        {
            _repository = repository;
        }

        public Task<long> CreateAsync(CreateShiftCalendarRequest request) => _repository.InsertAsync(request);

        public Task UpdateAsync(UpdateShiftCalendarRequest request) => _repository.UpdateAsync(request);

        public Task DeleteAsync(DeleteShiftCalendarRequest request) => _repository.DeleteAsync(request);

        public Task<IEnumerable<ShiftCalendarResponse>> GetAllAsync() => _repository.GetAllAsync();

        public Task<ShiftCalendarResponse?> GetByIdAsync(long id) => _repository.GetByIdAsync(id);

        public Task<ShiftCalendarResponse?> GetByDateAsync(long shiftId, DateOnly productionDate) => _repository.GetByDateAsync(shiftId, productionDate);
    }
}
