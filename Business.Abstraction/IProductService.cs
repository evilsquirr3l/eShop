using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models.DTO;

namespace Business.Abstraction
{
    public interface IProductService
    {
        Task Create(Product product); 
        Task<Product> GetById(int id);
        Task<IEnumerable<Product>> GetAll();
        Task Update(Product product);
        Task Delete(Product product);

    }
}