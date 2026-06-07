using DanaCopilot.Application;
using DanaCopilot.Domain;
using DanaCopilot.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.Services
{
    public class KnowledgeGapService
      : IKnowledgeGapService
    {
        private readonly
            IKnowledgeGapRepository _repository;

        public KnowledgeGapService(
            IKnowledgeGapRepository repository)
        {
            _repository = repository;
        }

        public async Task RegisterAsync(
            string question,
            string context)
        {
            var gap =
                new KnowledgeGap
                {
                    Question = question,
                    Context = context,
                    Frequency = 1,
                    Status = GapStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                };

            await _repository.CreateAsync(gap);
        }

        public async Task ResolveAsync(
            long gapId,
            string answer)
        {
            var gap =
                await _repository.GetByIdAsync(gapId);

            if (gap == null)
                return;

            gap.FinalAnswer = answer;

            gap.Status =
                GapStatus.Resolved;

            gap.ResolvedAt =
                DateTime.UtcNow;

            await _repository.UpdateAsync(gap);
        }

        public async Task RejectAsync(
            long gapId)
        {
            var gap =
                await _repository.GetByIdAsync(gapId);

            if (gap == null)
                return;

            gap.Status =
                GapStatus.Rejected;

            await _repository.UpdateAsync(gap);
        }
    }
}
