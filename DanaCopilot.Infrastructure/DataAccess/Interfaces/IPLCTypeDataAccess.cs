using DanaCopilot.Contracts.PLCType.Requests;
using DanaCopilot.Contracts.PLCType.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{

    public interface IPLCTypeDataAccess
    {
        Task<IEnumerable<PLCTypeResponse>> GetAllAsync();

        Task<PLCTypeResponse?> GetByIdAsync(long id);

        Task<long> InsertAsync(CreatePLCTypeRequest request);

        Task UpdateAsync(UpdatePLCTypeRequest request);

        Task DeleteAsync(DeletePLCTypeRequest request);
    }
}
