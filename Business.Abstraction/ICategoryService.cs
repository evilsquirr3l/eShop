using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Abstraction
{
    public interface ICategoryService
    {
        Task Create(CategoryDTO categoryDto);
        Task<IEnumerable<CategoryDTO>> GetAll();
        Task<CategoryDTO> GetById(int id);
        Task Update(CategoryDTO categoryDto);
        Task Delete(CategoryDTO categoryDto);
    }
}