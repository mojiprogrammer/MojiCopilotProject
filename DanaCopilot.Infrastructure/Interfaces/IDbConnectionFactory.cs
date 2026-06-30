using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DanaCopilot.Infrastructure.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create();
    }
}
