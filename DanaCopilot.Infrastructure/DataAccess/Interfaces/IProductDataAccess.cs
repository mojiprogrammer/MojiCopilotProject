using DanaCopilot.Contracts.Product.Requests;
using DanaCopilot.Contracts.Product.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface IProductDataAccess
    {
        Task<IEnumerable<ProductResponse>> GetAllAsync();

        Task<ProductResponse?> GetByIdAsync(long id);

        Task<long> InsertAsync(CreateProductRequest request);

        Task UpdateAsync(UpdateProductRequest request);

        Task DeleteAsync(DeleteProductRequest request);
    }
}
