using DanaCopilot.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DanaCopilot.Infrastructure
{
    public abstract class BaseRepository
    {
        protected readonly IDbConnectionFactory _factory;

        protected BaseRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        protected IDbConnection Connection => _factory.Create();
    }
}
