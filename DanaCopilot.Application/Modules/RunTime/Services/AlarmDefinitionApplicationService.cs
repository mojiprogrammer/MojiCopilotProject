using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.AlarmDefinition.Requests;
using DanaCopilot.Contracts.AlarmDefinition.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Modules.RunTime.Services
{
    public sealed class AlarmDefinitionApplicationService : IAlarmDefinitionApplicationService
    {
        private readonly IAlarmDefinitionDataAccess _repository;

        public AlarmDefinitionApplicationService(IAlarmDefinitionDataAccess repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<AlarmDefinitionResponse>> GetAllAsync() => _repository.GetAllAsync();

        public Task<AlarmDefinitionResponse?> GetByIdAsync(long id) => _repository.GetByIdAsync(id);

        public Task<IEnumerable<AlarmDefinitionResponse>> GetByParameterAsync(long parameterId) => _repository.GetByParameterAsync(parameterId);

        public Task<long> CreateAsync(CreateAlarmDefinitionRequest request) => _repository.InsertAsync(request);

        public Task UpdateAsync(UpdateAlarmDefinitionRequest request) => _repository.UpdateAsync(request);

        public Task DeleteAsync(DeleteAlarmDefinitionRequest request) => _repository.DeleteAsync(request);
    }
}
