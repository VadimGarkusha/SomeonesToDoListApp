using AndresToDoListApp.DataAccessLayer.Interfaces;
using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.DataAccessLayer.Entities;

namespace AndresToDoListApp.DataAccessLayer
{
    public class ToDoRepository : GenericRepository<ToDo>, IToDoRepository
    {
        public ToDoRepository(SomeonesToDoListContext context) : base(context) { }
    }
}
