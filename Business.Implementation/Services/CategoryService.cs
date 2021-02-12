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
    public class CategoryService : ICategoryService
    {
        private readonly ShopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<CategoryDto> _validator;

        public CategoryService(IMapper mapper, ShopDbContext dbContext, AbstractValidator<CategoryDto> validator)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task CreateAsync(CategoryDto categoryDto)
        {
            await _validator.ValidateAsync(categoryDto);
            await _dbContext.Categories.AddAsync(_mapper.Map<Category>(categoryDto));
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CategoryDto>(await _dbContext.Categories.FindAsync(id));
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task UpdateAsync(CategoryDto categoryDto)
        {
            await _validator.ValidateAsync(categoryDto);
            _dbContext.Categories.Update(_mapper.Map<Category>(categoryDto));
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CategoryDto categoryDto)
        {
            _dbContext.Categories.Remove(_mapper.Map<Category>(categoryDto));
            
            await _dbContext.SaveChangesAsync();
        }
    }
}