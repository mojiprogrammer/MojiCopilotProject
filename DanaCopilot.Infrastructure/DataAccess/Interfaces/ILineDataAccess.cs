using DanaCopilot.Contracts.Line.Requests;
using DanaCopilot.Contracts.Line.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface ILineDataAccess
    {
        Task<IEnumerable<LineResponse>> GetAllAsync();

        Task<LineResponse?> GetByIdAsync(long id);

        Task<long> InsertAsync(CreateLineRequest request);

        Task UpdateAsync(UpdateLineRequest request);

        Task DeleteAsync(DeleteLineRequest request);
    }
}
