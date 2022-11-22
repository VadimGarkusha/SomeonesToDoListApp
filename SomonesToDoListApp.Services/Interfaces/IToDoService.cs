using SomeonesToDoListApp.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SomeonesToDoListApp.Services.Interfaces
{
    /// <summary>
    /// Service for managing ToDo items
    /// </summary>
    public interface IToDoService
    {
        /// <summary>
        /// Creates a new to do list item asynchronously and returns created ToDo
        /// </summary>
        /// <param name="toDo">New ToDo item</param>
        /// <returns>Created ToDo ViewModel</returns>
        Task<ToDo> CreateToDoAsync(ToDo toDo);

        /// <summary>
        /// Retrieves a collection of all of the current to do list items asynchronously
        /// </summary>
        /// <returns>A collection of saved ToDos</returns>
        Task<IEnumerable<ToDo>> GetToDoItemsAsync();

        /// <summary>
        /// Updates a new to do list item asynchronously and returns updated ToDo
        /// </summary>
        /// <param name="toDo">New ToDo item</param>
        /// <returns>The result of the update</returns>
        Task<bool> UpdateToDoAsync(ToDo toDo);
    }
}
