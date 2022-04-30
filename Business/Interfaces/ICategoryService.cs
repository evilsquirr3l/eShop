using Business.Records;

namespace Business.Interfaces;

public interface ICategoryService
{
    public Task<CategoryRecord> GetCategoryAsync(int id);

    public Task<ResultSet<CategoryRecord>> GetCategoryListAsync(PaginationModel paginationModel);

    public Task CreateCategoryAsync(CategoryRecord categoryRecord);

    public Task UpdateCategoryAsync(int id, CategoryRecord categoryRecord);

    public Task DeleteCategoryAsync(int id);
}