using DanaCopilot.Contracts.ProductCategory.Requests;
using DanaCopilot.Contracts.ProductCategory.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{
    public sealed class ProductCategoryDataAccess : BaseDataAccess, IProductCategoryDataAccess
    {
        public ProductCategoryDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<IEnumerable<ProductCategoryResponse>> GetAllAsync()
        {
            return QueryAsync<ProductCategoryResponse>("core.sp_ProductCategory_GetAll");
        }

        public Task<ProductCategoryResponse?> GetByIdAsync(long id)
        {
            return QueryFirstOrDefaultAsync<ProductCategoryResponse>("core.sp_ProductCategory_GetById",
                new
                {
                    Id = id
                });
        }

        public Task<long> InsertAsync(CreateProductCategoryRequest request)
        {
            return ExecuteScalarAsync<long>("core.sp_ProductCategory_Insert", request);
        }

        public Task UpdateAsync(UpdateProductCategoryRequest request)
        {
            return ExecuteAsync("core.sp_ProductCategory_Update", request);
        }

        public Task DeleteAsync(DeleteProductCategoryRequest request)
        {
            return ExecuteAsync("core.sp_ProductCategory_Delete", request);
        }
    }
}
