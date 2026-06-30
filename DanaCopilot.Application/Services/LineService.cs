using DanaCopilot.Contracts.DTOs;
using DanaCopilot.Infrastructure.Services.TestLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Services
{
    public class LineService
    {
        private readonly LineRepository _repo;

        public LineService(LineRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<LineDto>> GetAll()=> _repo.GetAll();

        public Task<long> Save(LineDto dto)=> _repo.Upsert(dto);
    }
}
