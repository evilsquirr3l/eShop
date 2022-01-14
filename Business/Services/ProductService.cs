using AutoMapper;
using Business.Interfaces;
using Business.Records;
using Data;
using Data.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class ProductService : IProductService
{
    private readonly EShopDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductRecord> _validator;

    public ProductService(EShopDbContext dbContext, IMapper mapper, IValidator<ProductRecord> validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ProductRecord> GetProductAsync(int id)
    {
        var product = await _dbContext.Products
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
        var productRecord = _mapper.Map<ProductRecord>(product);
        
        return productRecord;
    }

    public Task<IEnumerable<ProductRecord>> GetProductsListAsync()
    {
        throw new NotImplementedException();
    }

    public async Task CreateProductAsync(ProductRecord productRecord)
    {
        await _validator.ValidateAndThrowAsync(productRecord);
        var product = _mapper.Map<Product>(productRecord);

        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
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