using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstraction;
using Business.Models;
using Data.Entities;
using Data.Implementation;

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
            _dbContext.Products.Add((_mapper.Map<Product>(productDto))); 
            _dbContext.SaveChanges();
        }

        public async Task<ProductDTO> GetById(int id)
        {
            return  _mapper.Map<ProductDTO>(_dbContext.Products.Find(id));
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var response = _dbContext.Products.Select(x => _mapper.Map<ProductDTO>(x));
            return response;
        }

        public async Task Update(ProductDTO productDto)
        {
            _dbContext.Products.Update(_mapper.Map<Product>(productDto));
            _dbContext.SaveChanges();
        }

        public async Task Delete(ProductDTO productDto)
        {
            _dbContext.Products.Remove(_mapper.Map<Product>(productDto));
            _dbContext.SaveChanges();
        }
    }
}