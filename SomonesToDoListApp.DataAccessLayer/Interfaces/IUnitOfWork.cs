using System;
using System.Threading.Tasks;

namespace AndresToDoListApp.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IToDoRepository ToDoRepository { get; }
        Task<int> Save();
    }
}
