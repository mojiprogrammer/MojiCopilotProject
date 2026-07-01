using DanaCopilot.Contracts.Product.Requests;
using DanaCopilot.Contracts.Product.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{
    public sealed class ProductDataAccess : BaseDataAccess, IProductDataAccess
    {
        public ProductDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<IEnumerable<ProductResponse>> GetAllAsync() => QueryAsync<ProductResponse>("core.sp_Product_GetAll");

        public Task<ProductResponse?> GetByIdAsync(long id) => QueryFirstOrDefaultAsync<ProductResponse>("core.sp_Product_GetById",
                new { Id = id });

        public Task<long> InsertAsync(CreateProductRequest request) => ExecuteScalarAsync<long>("core.sp_Product_Insert", request);

        public Task UpdateAsync(UpdateProductRequest request) => ExecuteAsync("core.sp_Product_Update", request);

        public Task DeleteAsync(DeleteProductRequest request) => ExecuteAsync("core.sp_Product_Delete", request);
    }
}
