using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Abstraction
{
    public interface IProductService
    {
        Task Create(ProductDTO productDto); 
        Task<ProductDTO> GetById(int id);
        Task<IEnumerable<ProductDTO>> GetAll();
        Task Update(ProductDTO productDto);
        Task Delete(ProductDTO productDto);

    }
}