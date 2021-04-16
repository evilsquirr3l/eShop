using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstraction;
using Business.Models;
using Data.Entities;
using Data.Implementation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Business.Implementation.Services
{
    public class ProductService : ICrudInterface<ProductDto>
    {
        private readonly ShopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<ProductDto> _validator;
        private readonly IServiceHelper<Product> _helper;

        public ProductService(IMapper mapper, ShopDbContext dbContext, AbstractValidator<ProductDto> validator, IServiceHelper<Product> helper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _validator = validator;
            _helper = helper;
        }

        public async Task AddAsync(ProductDto model)
        {
            await _validator.ValidateAsync(model);
            await _dbContext.Products.AddAsync(_mapper.Map<Product>(model));
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            return _mapper.Map<ProductDto>(await _dbContext.Products.FindAsync(id));
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _dbContext.Products.ToListAsync();
            
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task UpdateAsync(int id, ProductDto model)
        {
            _helper.ThrowValidationExceptionIfModelIsNull(await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id));

            await _validator.ValidateAsync(model);
            _dbContext.Products.Update(_mapper.Map<Product>(model));

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            _helper.ThrowValidationExceptionIfModelIsNull(product);

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}