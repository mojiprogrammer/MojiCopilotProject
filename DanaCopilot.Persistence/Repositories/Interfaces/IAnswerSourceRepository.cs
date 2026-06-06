using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Repositories.Interfaces
{
    public interface IAnswerSourceRepository
    {
        Task<long> CreateAsync(AnswerSource source);

        Task<List<AnswerSource>> GetByMessageIdAsync(
            long messageId);

        Task CreateManyAsync(
            List<AnswerSource> sources);
    }
}
