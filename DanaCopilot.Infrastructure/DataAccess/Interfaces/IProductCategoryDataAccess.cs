using DanaCopilot.Contracts.ProductCategory.Requests;
using DanaCopilot.Contracts.ProductCategory.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface IProductCategoryDataAccess
    {
        Task<IEnumerable<ProductCategoryResponse>> GetAllAsync();

        Task<ProductCategoryResponse?> GetByIdAsync(long id);

        Task<long> InsertAsync(CreateProductCategoryRequest request);

        Task UpdateAsync(UpdateProductCategoryRequest request);

        Task DeleteAsync(DeleteProductCategoryRequest request);
    }
}
