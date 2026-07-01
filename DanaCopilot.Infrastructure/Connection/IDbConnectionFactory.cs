using System.Data;

namespace DanaCopilot.Infrastructure.Connection
{
     public interface IDbConnectionFactory
    {
        IDbConnection Create();
    }
}
