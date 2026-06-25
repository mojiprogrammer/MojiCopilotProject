using DanaCopilot.Domain.Entities;
using Dapper;
using System.Data;

namespace DanaCopilot.Application.Services
{
    public class ProductService
    {
        private readonly IDbConnection _db;

        public ProductService(IDbConnection db)
        {
            _db = db;
        }

        public Task<long> CreateAsync(Product product)
        {
            return _db.ExecuteScalarAsync<long>(
                "Config.usp_Product_Create",
                product,
                commandType: CommandType.StoredProcedure);
        }
    }
}
