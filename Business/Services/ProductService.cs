using AutoMapper;
using Business.Interfaces;
using Business.Paging;
using Business.Records;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class ProductService : IProductService
{
    private readonly EShopDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ProductService(EShopDbContext dbContext, IMapper mapper, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ProductRecord> GetProductAsync(int id)
    {
        var product = await _dbContext.Products
            .Include(x => x.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

        return _mapper.Map<ProductRecord>(product);
    }

    public async Task<PagedList<ProductRecord>> GetProductsListAsync(QueryStringParameters queryStringParameters)
    {
        var products = _dbContext.Products;
        
        var selectedProducts = products.OrderBy(x => x.Name).Paginate(queryStringParameters);;
        var mappedProducts = _mapper.Map<List<ProductRecord>>(selectedProducts);

        return PagedList<ProductRecord>.ToPagedList(mappedProducts,
            await products.CountAsync(),
            queryStringParameters.CurrentPage,
            queryStringParameters.PageSize);
    }

    public async Task CreateProductAsync(ProductRecord productRecord)
    {
        productRecord.CreatedAt = _dateTimeProvider.GetCurrentTime();
        productRecord.ModifiedAt = _dateTimeProvider.GetCurrentTime();
        var product = _mapper.Map<Product>(productRecord);

        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        productRecord.Id = product.Id;
    }

    public async Task UpdateProductAsync(int id, ProductRecord productRecord)
    {
        productRecord.ModifiedAt = _dateTimeProvider.GetCurrentTime();
        _dbContext.Products.Update(_mapper.Map<Product>(productRecord));
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _dbContext.Products.FindAsync(id);

        product.ModifiedAt = _dateTimeProvider.GetCurrentTime();
        product.IsDeleted = true;
        _dbContext.Entry(product).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}