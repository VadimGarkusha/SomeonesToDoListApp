using SomeonesToDoListApp.DataAccessLayer.Entities;
using System.Data.Entity;

namespace SomeonesToDoListApp.DataAccessLayer.Interfaces
{
    public interface ISomeonesToDoListContext
    {
        DbSet<ToDo> ToDos { get; set; }
    }
}
