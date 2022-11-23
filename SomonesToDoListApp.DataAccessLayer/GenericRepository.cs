using AndresToDoListApp.DataAccessLayer.Interfaces;
using SomeonesToDoListApp.DataAccessLayer.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AndresToDoListApp.DataAccessLayer
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private SomeonesToDoListContext _context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(SomeonesToDoListContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual Task<TEntity> GetByID(object id)
        {
            return dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public virtual TEntity Add(TEntity entity)
        {
            return dbSet.Add(entity);
        }

        public virtual async Task Delete(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _context.SetModified(entityToUpdate);
        }
    }
}
