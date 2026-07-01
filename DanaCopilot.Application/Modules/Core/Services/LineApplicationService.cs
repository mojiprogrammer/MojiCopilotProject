using DanaCopilot.Application.Modules.Core.Interfaces;
using DanaCopilot.Contracts.Line.Requests;
using DanaCopilot.Contracts.Line.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace TestSystem.Application.Modules.Configuration.Line.Services;

public sealed class LineApplicationService : ILineApplicationService
{
    private readonly ILineDataAccess _lineDataAccess;

    public LineApplicationService(ILineDataAccess lineDataAccess)
    {
        _lineDataAccess = lineDataAccess;
    }

    public async Task<IEnumerable<LineResponse>> GetAllAsync()
    {
        return await _lineDataAccess.GetAllAsync();
    }

    public async Task<LineResponse?> GetByIdAsync(long id)
    {
        return await _lineDataAccess.GetByIdAsync(id);
    }

    public async Task<long> CreateAsync(CreateLineRequest request)
    {
        return await _lineDataAccess.InsertAsync(request);
    }

    public async Task UpdateAsync(UpdateLineRequest request)
    {
        await _lineDataAccess.UpdateAsync(request);
    }

    public async Task DeleteAsync(DeleteLineRequest request)
    {
        await _lineDataAccess.DeleteAsync(request);
    }
}