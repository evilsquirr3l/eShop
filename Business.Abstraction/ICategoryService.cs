using System.Collections.Generic;
using Business.Models.DTO;

namespace Business.Abstraction
{
    public interface ICategoryService
    {
        void Create(Category Category);
        ICollection<Category> GetAll();
        Category GetById(int id);
        void Update(Category Category);
        void Delete(Category Category);
        void Delete(int id);
    }
}