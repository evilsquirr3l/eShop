using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstraction;
using Business.Implementation.Validations;
using Business.Models;
using Data.Entities;
using Data.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Business.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ShopDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, ShopDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task CreateAsync(ProductDto productDto)
        {
            ProductValidation.ValidateProduct(productDto);
            await _dbContext.Products.AddAsync(_mapper.Map<Product>(productDto));
            
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

        public async Task UpdateAsync(ProductDto productDto)
        {
            ProductValidation.ValidateProduct(productDto);
            _dbContext.Products.Update(_mapper.Map<Product>(productDto));
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductDto productDto)
        {
            _dbContext.Products.Remove(_mapper.Map<Product>(productDto));
            
            await _dbContext.SaveChangesAsync();
        }
    }
}