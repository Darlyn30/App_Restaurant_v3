using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel>
        where ViewModel : class
        where SaveViewModel : class
    {
        Task Update(SaveViewModel vm);
        Task<SaveViewModel> Add(SaveViewModel vm);
        Task delete(int id);
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task<IEnumerable<SaveViewModel>> GetAllViewModel();

    }
}
