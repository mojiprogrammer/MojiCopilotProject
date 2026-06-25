using DanaCopilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.Interfaces
{
    public interface ITagMappingRepository
    {
        Task<IEnumerable<TagMapping>> GetByDeviceAsync(long deviceId);
    }
}
