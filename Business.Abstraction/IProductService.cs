using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models.DTO;

namespace Business.Abstraction
{
    public interface IProductService
    {
        Task Create(Product product); ICollection<Product> GetAll();
        Task<Product> GetById(int id);
        Task<IEnumerable<Product>> GetAll();
        Task Update(int id);
        Task Delete(Product product);

    }
}