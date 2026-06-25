using DanaCopilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.Interfaces
{
    public interface ITestProfileRepository
    {
        Task<long> CreateAsync(TestProfile profile);
        Task<IEnumerable<TestStep>> GetStepsAsync(long profileId);
    }
}
