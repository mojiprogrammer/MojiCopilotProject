using DanaCopilot.Domain.Entities;
using DanaCopilot.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Services
{
    public class TestProfileService
    {
        private readonly ITestProfileRepository _repo;

        public TestProfileService(ITestProfileRepository repo)
        {
            _repo = repo;
        }

        public Task<long> CreateAsync(TestProfile profile)
            => _repo.CreateAsync(profile);

        public Task<IEnumerable<TestStep>> GetStepsAsync(long profileId)
            => _repo.GetStepsAsync(profileId);
    }
}
