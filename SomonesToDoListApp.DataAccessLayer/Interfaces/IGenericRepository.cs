using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndresToDoListApp.DataAccessLayer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByID(object id);

        Task<IEnumerable<T>> GetAll();

        T Add(T entity);

        Task Delete(object id);

        void Delete(T entityToDelete);

        void Update(T entityToUpdate);
    }
}
