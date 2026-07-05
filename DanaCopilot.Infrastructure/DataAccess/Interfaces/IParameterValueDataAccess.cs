using DanaCopilot.Infrastructure.Models;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface IParameterValueDataAccess
    {
        Task InsertAsync(long parameterId, long plcId, long? stationId, decimal numericValue, string value, DateTime timestamp);

        Task BulkInsertAsync(IEnumerable<ParameterValueInsertModel> values);
    }
}
