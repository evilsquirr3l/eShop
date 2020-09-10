using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models.DTO;

namespace Business.Abstraction
{
    public interface ICartService
    {
        Task Create(Cart Cart);
        Task<IEnumerable<Cart>> GetAll();
        Task<Cart> GetById(int id);
        Task Update(Cart Cart);
        Task Delete(Cart Cart);
    }
}