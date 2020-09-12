using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Abstraction
{
    public interface ICartService
    {
        Task Create(CartDto cart);
        Task<IEnumerable<CartDto>> GetAll();
        Task<CartDto> GetById(int id);
        Task Update(CartDto cart);
        Task Delete(CartDto cart);
    }
}