using AutoMapper;
using Business.Interfaces;
using Business.Paging;
using Business.Records;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class CategoryService : ICategoryService
{
    private readonly EShopDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CategoryService(EShopDbContext dbContext, IMapper mapper, IDateTimeProvider dateTimeProvider)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<CategoryRecord> GetCategoryAsync(int id)
    {
        var category = await _dbContext.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

        return _mapper.Map<CategoryRecord>(category);
    }

    public async Task<ResultSet<CategoryRecord>> GetCategoryListAsync(PaginationModel paginationModel)
    {
        var categories = _dbContext.Categories;
        
        var selectedCategories = categories.Paginate(paginationModel).OrderBy(x => x.Name);
        var mappedCategories = _mapper.Map<List<CategoryRecord>>(selectedCategories);

        return new ResultSet<CategoryRecord>
        {
            Data = mappedCategories,
            Page = new Page
            {
                Count = mappedCategories.Count,
                Skip = paginationModel.Skip,
                Take = paginationModel.Take,
                Total = await categories.CountAsync()
            }
        };
    }

    public async Task CreateCategoryAsync(CategoryRecord categoryRecord)
    {
        categoryRecord.CreatedAt = _dateTimeProvider.GetCurrentTime();
        categoryRecord.ModifiedAt = _dateTimeProvider.GetCurrentTime();
        var category = _mapper.Map<Category>(categoryRecord);

        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
        categoryRecord.Id = category.Id;
    }

    public async Task UpdateCategoryAsync(int id, CategoryRecord categoryRecord)
    {
        categoryRecord.ModifiedAt = _dateTimeProvider.GetCurrentTime();
        _dbContext.Categories.Update(_mapper.Map<Category>(categoryRecord));

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);

        category.ModifiedAt = _dateTimeProvider.GetCurrentTime();
        category.IsDeleted = true;
        _dbContext.Entry(category).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}