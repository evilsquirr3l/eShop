using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstraction;
using Business.Models;
using Data.Entities;
using Data.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Business.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ShopDbContext _dbContext;

        public CategoryService(IMapper mapper, ShopDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task Create(CategoryDto categoryDto)
        {
            await _dbContext.Categories.AddAsync((_mapper.Map<Category>(categoryDto)));
            await _dbContext.SaveChangesAsync();
        }
        public async Task<CategoryDto> GetById(int id)
        {
            return _mapper.Map<CategoryDto>(await _dbContext.Categories.FindAsync(id));
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task Update(CategoryDto categoryDto)
        {
            _dbContext.Categories.Update(_mapper.Map<Category>(categoryDto));
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(CategoryDto categoryDto)
        {
            _dbContext.Categories.Remove(_mapper.Map<Category>(categoryDto));
           await _dbContext.SaveChangesAsync();
        }
    }
}