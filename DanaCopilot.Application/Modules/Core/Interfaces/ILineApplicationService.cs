using DanaCopilot.Contracts.Line.Requests;
using DanaCopilot.Contracts.Line.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Modules.Core.Interfaces
{
    public interface ILineApplicationService
    {
        Task<IEnumerable<LineResponse>> GetAllAsync();

        Task<LineResponse?> GetByIdAsync(long id);

        Task<long> CreateAsync(CreateLineRequest request);

        Task UpdateAsync(UpdateLineRequest request);

        Task DeleteAsync(DeleteLineRequest request);
    }
}
