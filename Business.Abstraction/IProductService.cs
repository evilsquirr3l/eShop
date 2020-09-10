using System.Collections.Generic;
using Business.Models.DTO;

namespace Business.Abstraction
{
    public interface IProductService
    {
        void Create(Product product);
        ICollection<Product> GetAll();
        Product GetById(int id);
        void Update(int id);
        void Delete(Product product);

    }
}