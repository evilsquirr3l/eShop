using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Abstraction
{
    public interface ICartService
    {
        Task Create(CartDTO cart);
        Task<IEnumerable<CartDTO>> GetAll();
        Task<CartDTO> GetById(int id);
        Task Update(CartDTO cart);
        Task Delete(CartDTO cart);
    }
}