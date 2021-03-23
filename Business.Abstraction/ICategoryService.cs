using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Abstraction
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryDto categoryDto);

        Task<CategoryDto> GetByIdAsync(int id);

        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task UpdateAsync(CategoryDto categoryDto);

        Task DeleteAsync(CategoryDto categoryDto);
    }
}