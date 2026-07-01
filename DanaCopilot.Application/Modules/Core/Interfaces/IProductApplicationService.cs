using DanaCopilot.Contracts.Product.Requests;
using DanaCopilot.Contracts.Product.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Modules.Core.Interfaces
{
    public interface IProductApplicationService
    {
        Task<IEnumerable<ProductResponse>> GetAllAsync();

        Task<ProductResponse?> GetByIdAsync(long id);

        Task<long> CreateAsync(CreateProductRequest request);

        Task UpdateAsync(UpdateProductRequest request);

        Task DeleteAsync(DeleteProductRequest request);
    }
}
