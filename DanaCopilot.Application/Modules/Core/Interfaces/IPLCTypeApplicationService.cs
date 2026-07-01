using DanaCopilot.Contracts.PLCType.Requests;
using DanaCopilot.Contracts.PLCType.Responses;

namespace DanaCopilot.Application.Modules.Core.Interfaces
{
    public interface IPLCTypeApplicationService
    {
        Task<IEnumerable<PLCTypeResponse>> GetAllAsync();

        Task<PLCTypeResponse?> GetByIdAsync(long id);

        Task<long> CreateAsync(CreatePLCTypeRequest request);

        Task UpdateAsync(UpdatePLCTypeRequest request);

        Task DeleteAsync(DeletePLCTypeRequest request);
    }
}
