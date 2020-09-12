using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Abstraction
{
    public interface ICategoryService
    {
        Task Create(CategoryDto categoryDto);
        Task<IEnumerable<CategoryDto>> GetAll();
        Task<CategoryDto> GetById(int id);
        Task Update(CategoryDto categoryDto);
        Task Delete(CategoryDto categoryDto);
    }
}