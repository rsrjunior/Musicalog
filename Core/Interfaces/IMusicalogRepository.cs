using Musicalog.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Musicalog.Core.Interfaces
{
    public interface IMusicalogRepository<T> where T : EntityBase
    {
        Task<T> Create(T entity);
        Task<bool> Delete(int id);
        Task<bool> Edit(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(IDictionary<string,dynamic> parameters);
        Task<T> GetById(int id);
    }
}
