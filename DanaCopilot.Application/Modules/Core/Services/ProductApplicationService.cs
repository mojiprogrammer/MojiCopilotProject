using DanaCopilot.Application.Modules.Core.Interfaces;
using DanaCopilot.Contracts.Product.Requests;
using DanaCopilot.Contracts.Product.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Application.Modules.Core.Services
{
    public sealed class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductDataAccess _dataAccess;

        public ProductApplicationService(IProductDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<IEnumerable<ProductResponse>> GetAllAsync() => _dataAccess.GetAllAsync();

        public Task<ProductResponse?> GetByIdAsync(long id) => _dataAccess.GetByIdAsync(id);

        public Task<long> CreateAsync(CreateProductRequest request) => _dataAccess.InsertAsync(request);

        public Task UpdateAsync(UpdateProductRequest request) => _dataAccess.UpdateAsync(request);

        public Task DeleteAsync(DeleteProductRequest request) => _dataAccess.DeleteAsync(request);
    }
}
