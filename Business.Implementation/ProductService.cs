using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstraction;
using Business.Models;
using Data.Entities;
using Data.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Business.Implementation
{
    public class ProductService :IProductService
    {
        private readonly IMapper _mapper;
        private readonly ShopDbContext _dbContext;

        public ProductService(IMapper mapper, ShopDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task Create(ProductDTO productDto)
        { 
          await  _dbContext.Products.AddAsync((_mapper.Map<Product>(productDto))); 
          await _dbContext.SaveChangesAsync();
        }

        public async Task<ProductDTO> GetById(int id)
        {
            return  _mapper.Map<ProductDTO>(_dbContext.Products.FindAsync(id));
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var products = await _dbContext.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task Update(ProductDTO productDto)
        {
            _dbContext.Products.Update(_mapper.Map<Product>(productDto));
          await  _dbContext.SaveChangesAsync();
        }

        public async Task Delete(ProductDTO productDto)
        {
            _dbContext.Products.Remove(_mapper.Map<Product>(productDto));
           await _dbContext.SaveChangesAsync();
        }
    }
}