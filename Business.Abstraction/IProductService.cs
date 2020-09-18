using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Abstraction
{
    public interface IProductService
    {
        Task CreateAsync(ProductDto productDto);

        Task<ProductDto> GetByIdAsync(int id);

        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task UpdateAsync(ProductDto productDto);

        Task DeleteAsync(ProductDto productDto);
    }
}