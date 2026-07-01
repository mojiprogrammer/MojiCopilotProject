using DanaCopilot.Contracts.ProductCategory.Requests;
using DanaCopilot.Contracts.ProductCategory.Responses;

namespace DanaCopilot.Application.Modules.Core.Interfaces
{
    public interface IProductCategoryApplicationService
    {
        Task<IEnumerable<ProductCategoryResponse>> GetAllAsync();

        Task<ProductCategoryResponse?> GetByIdAsync(long id);

        Task<long> CreateAsync(CreateProductCategoryRequest request);

        Task UpdateAsync(UpdateProductCategoryRequest request);

        Task DeleteAsync(DeleteProductCategoryRequest request);
    }
}
