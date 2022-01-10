using AutoMapper;
using Business.Interfaces;
using Business.Records;
using Database;

namespace Business.Services;

public class ProductService : IProductService
{
    private readonly EShopDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductService(EShopDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ProductRecord> GetProductAsync(int id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        var productRecord = _mapper.Map<ProductRecord>(product);
        
        return productRecord;
    }

    public Task<IEnumerable<ProductRecord>> GetProductsListAsync()
    {
        throw new NotImplementedException();
    }

    public Task CreateProduct(ProductRecord productRecord)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProductAsync(int id, ProductRecord productRecord)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(int id)
    {
        throw new NotImplementedException();
    }
}