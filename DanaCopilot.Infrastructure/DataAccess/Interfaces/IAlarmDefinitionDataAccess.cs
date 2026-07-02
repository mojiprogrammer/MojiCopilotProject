using DanaCopilot.Contracts.AlarmDefinition.Requests;
using DanaCopilot.Contracts.AlarmDefinition.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{

    public interface IAlarmDefinitionDataAccess
    {
        Task<IEnumerable<AlarmDefinitionResponse>> GetAllAsync();

        Task<AlarmDefinitionResponse?> GetByIdAsync(long id);

        Task<IEnumerable<AlarmDefinitionResponse>> GetByParameterAsync(long parameterId);

        Task<long> InsertAsync(CreateAlarmDefinitionRequest request);

        Task UpdateAsync(UpdateAlarmDefinitionRequest request);

        Task DeleteAsync(DeleteAlarmDefinitionRequest request);
    }
}
