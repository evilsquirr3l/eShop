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
    public class CategoryService : ICrudInterface<CategoryDto>
    {
        private readonly ShopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<CategoryDto> _validator;
        private readonly IServiceHelper<Category> _helper;

        public CategoryService(IMapper mapper, ShopDbContext dbContext, AbstractValidator<CategoryDto> validator, IServiceHelper<Category> helper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _validator = validator;
            _helper = helper;
        }

        public async Task AddAsync(CategoryDto model)
        {
            await _validator.ValidateAsync(model);
            await _dbContext.Categories.AddAsync(_mapper.Map<Category>(model));
            
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

        public async Task UpdateAsync(int id, CategoryDto model)
        {
            _helper.ThrowValidationExceptionIfModelIsNull(await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id));

            await _validator.ValidateAsync(model);
            _dbContext.Categories.Update(_mapper.Map<Category>(model));

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            _helper.ThrowValidationExceptionIfModelIsNull(category);

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}