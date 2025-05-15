using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T model);
        Task UpdateAsync(T model);
        Task DeleteAsync(int id);
        Task<T> GetByIdAsync(int id);
    }
}
