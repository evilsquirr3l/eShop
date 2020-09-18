using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Abstraction
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryDto categoryDto);

        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task<CategoryDto> GetByIdAsync(int id);

        Task UpdateAsync(CategoryDto categoryDto);

        Task DeleteAsync(CategoryDto categoryDto);
    }
}