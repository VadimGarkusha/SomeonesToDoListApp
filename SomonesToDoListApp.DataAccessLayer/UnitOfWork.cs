using AndresToDoListApp.DataAccessLayer.Interfaces;
using SomeonesToDoListApp.DataAccessLayer.Context;
using System;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace AndresToDoListApp.DataAccessLayer
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly SomeonesToDoListContext _context;
        private IToDoRepository _toDoRepository;

        public IToDoRepository ToDoRepository
        {
            get
            {
                if(_toDoRepository == null)
                {
                    _toDoRepository = new ToDoRepository(_context);
                }
                return _toDoRepository;
            }
        }

        public UnitOfWork(SomeonesToDoListContext bookStoreDbContext)
        {
            _context = bookStoreDbContext;
        }

        public Task<int> Save()
        {
            return _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
