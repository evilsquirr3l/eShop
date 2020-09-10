using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models.DTO;

namespace Business.Abstraction
{
    public interface ICategoryService
    {
        Task Create(Category Category);
        Task<ICollection<Category>> GetAll();
        Task<Category> GetById(int id);
        Task Update(Category Category);
        Task Delete(Category Category);
    }
}