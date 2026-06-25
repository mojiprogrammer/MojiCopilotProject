using DanaCopilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.Interfaces
{
    public interface IDeviceRepository
    {
        Task<long> CreateAsync(Device device);
        Task<Device?> GetByIdAsync(long id);
    }
}
