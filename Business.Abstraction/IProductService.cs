using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Abstraction
{
    public interface IProductService
    {
        Task Create(ProductDto productDto);

        Task<ProductDto> GetById(int id);

        Task<IEnumerable<ProductDto>> GetAll();

        Task Update(ProductDto productDto);

        Task Delete(ProductDto productDto);
    }
}