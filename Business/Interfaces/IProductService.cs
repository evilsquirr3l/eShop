using Business.Paging;
using Business.Records;

namespace Business.Interfaces;

public interface IProductService
{
    public Task<ProductRecord> GetProductAsync(int id);

    public Task<PagedList<ProductRecord>> GetProductsListAsync(QueryStringParameters queryStringParameters);

    public Task CreateProductAsync(ProductRecord productRecord);

    public Task UpdateProductAsync(int id, ProductRecord productRecord);

    public Task DeleteProductAsync(int id);
}