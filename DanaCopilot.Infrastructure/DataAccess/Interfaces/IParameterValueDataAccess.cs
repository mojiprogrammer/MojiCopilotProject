using DanaCopilot.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface IParameterValueDataAccess
    {
        Task InsertAsync(long parameterId, long plcId, long? stationId, decimal numericValue, string value, DateTime timestamp);

        Task BulkInsertAsync(IEnumerable<ParameterValueInsertModel> values);
    }
}
