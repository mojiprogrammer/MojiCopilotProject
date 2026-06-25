using DanaCopilot.Domain.Entities;
using DanaCopilot.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Services
{
    public class DeviceService
    {
        private readonly IDeviceRepository _repo;

        public DeviceService(IDeviceRepository repo)
        {
            _repo = repo;
        }

        public Task<long> CreateAsync(Device device) => _repo.CreateAsync(device);
    }
}
