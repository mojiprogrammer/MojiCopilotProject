using Dapper;
using System.Data;

namespace DanaCopilot.Infrastructure.Connection
{

    public abstract class BaseDataAccess
    {
        private readonly IDbConnectionFactory _connectionFactory;

        protected BaseDataAccess(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        protected IDbConnection CreateConnection()
        {
            return _connectionFactory.Create();
        }

        #region Query

        protected async Task<IEnumerable<T>> QueryAsync<T>(
            string storedProcedure,
            object? parameters = null)
        {
            using var connection = CreateConnection();

            return await connection.QueryAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        protected async Task<T?> QueryFirstOrDefaultAsync<T>(
            string storedProcedure,
            object? parameters = null)
        {
            using var connection = CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        protected async Task<T> QueryFirstAsync<T>(
            string storedProcedure,
            object? parameters = null)
        {
            using var connection = CreateConnection();

            return await connection.QueryFirstAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        #endregion

        #region Execute

        protected async Task ExecuteAsync(
            string storedProcedure,
            object? parameters = null)
        {
            using var connection = CreateConnection();

            await connection.ExecuteAsync(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        protected async Task<int> ExecuteReturnRowAsync(
            string storedProcedure,
            object? parameters = null)
        {
            using var connection = CreateConnection();

            return await connection.ExecuteAsync(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        protected async Task<T> ExecuteScalarAsync<T>(
            string storedProcedure,
            object? parameters = null)
        {
            using var connection = CreateConnection();

            return await connection.ExecuteScalarAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        #endregion
    }
}
