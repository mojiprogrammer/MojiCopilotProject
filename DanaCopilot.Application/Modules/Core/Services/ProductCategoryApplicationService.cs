using DanaCopilot.Application.Modules.Core.Interfaces;
using DanaCopilot.Contracts.ProductCategory.Requests;
using DanaCopilot.Contracts.ProductCategory.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Application.Modules.Core.Services
{
 

    public sealed class ProductCategoryApplicationService: IProductCategoryApplicationService
    {
        private readonly IProductCategoryDataAccess _dataAccess;

        public ProductCategoryApplicationService(IProductCategoryDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<IEnumerable<ProductCategoryResponse>> GetAllAsync()=> _dataAccess.GetAllAsync();

        public Task<ProductCategoryResponse?> GetByIdAsync(long id)=> _dataAccess.GetByIdAsync(id);

        public Task<long> CreateAsync(CreateProductCategoryRequest request)=> _dataAccess.InsertAsync(request);

        public Task UpdateAsync(UpdateProductCategoryRequest request)=> _dataAccess.UpdateAsync(request);

        public Task DeleteAsync(DeleteProductCategoryRequest request)=> _dataAccess.DeleteAsync(request);
    }
}
