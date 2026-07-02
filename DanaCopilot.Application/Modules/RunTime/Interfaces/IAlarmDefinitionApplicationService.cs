using DanaCopilot.Contracts.AlarmDefinition.Requests;
using DanaCopilot.Contracts.AlarmDefinition.Responses;

namespace DanaCopilot.Application.Modules.RunTime.Interfaces
{

    public interface IAlarmDefinitionApplicationService
    {
        Task<IEnumerable<AlarmDefinitionResponse>> GetAllAsync();

        Task<AlarmDefinitionResponse?> GetByIdAsync(long id);

        Task<IEnumerable<AlarmDefinitionResponse>> GetByParameterAsync(long parameterId);

        Task<long> CreateAsync(CreateAlarmDefinitionRequest request);

        Task UpdateAsync(UpdateAlarmDefinitionRequest request);

        Task DeleteAsync(DeleteAlarmDefinitionRequest request);
    }
}
