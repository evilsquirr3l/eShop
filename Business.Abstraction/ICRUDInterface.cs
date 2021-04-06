using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Abstraction
{
    public interface ICrudInterface<T> where T: class
    {
        Task AddAsync(T model);
        
        Task<T> GetByIdAsync(int id);
        
        Task<IEnumerable<T>> GetAllAsync();
        
        Task UpdateAsync(int id, T model);
        
        Task DeleteByIdAsync(int id);
    }
}