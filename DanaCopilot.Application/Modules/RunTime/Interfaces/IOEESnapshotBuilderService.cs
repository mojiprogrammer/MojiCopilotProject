using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Modules.RunTime.Interfaces
{
    public interface IOEESnapshotBuilderService
    {
        Task<long> BuildAsync(long productionLineId, DateOnly date);
    }
}
