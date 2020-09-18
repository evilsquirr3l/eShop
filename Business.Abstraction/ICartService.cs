using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Abstraction
{
    public interface ICartService
    {
        Task CreateAsync(CartDto cartDto);
        
        Task<CartDto> GetByIdAsync(int id);
        
        Task<IEnumerable<CartDto>> GetAllAsync();
        
        Task UpdateAsync(CartDto cartDto);
        
        Task DeleteAsync(CartDto cartDto);
    }
}