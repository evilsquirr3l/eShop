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
    private readonly IDateTimeProvider _dateTimeProvider;

    public ProductService(EShopDbContext dbContext, IMapper mapper, IValidator<ProductRecord> validator, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ProductRecord> GetProductAsync(int id)
    {
        var product = await _dbContext.Products
            .Include(x => x.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
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
        productRecord.CreatedAt = _dateTimeProvider.GetCurrentTime();
        var product = _mapper.Map<Product>(productRecord);

        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        productRecord.Id = product.Id;
    }

    public async Task UpdateProductAsync(int id, ProductRecord productRecord)
    {
        await _validator.ValidateAndThrowAsync(productRecord);

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