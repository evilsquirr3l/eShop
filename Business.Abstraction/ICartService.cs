using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Abstraction
{
    public interface ICartService
    {
        Task Create(CartDTO Cart);
        Task<IEnumerable<CartDTO>> GetAll();
        Task<CartDTO> GetById(int id);
        Task Update(CartDTO Cart);
        Task Delete(CartDTO Cart);
    }
}