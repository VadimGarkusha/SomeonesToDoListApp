using SomeonesToDoListApp.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.ViewModels;
using AutoMapper;
using System;
using AndresToDoListApp;

namespace SomeonesToDoListApp.Controllers
{
    [RoutePrefix("ToDo")]
    public class ToDoController : ApiController
    {
        private IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        /// <summary>
        /// An HTTP Post request to create a new to do item
        /// </summary>
        /// <param name="toDoVm"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateToDo")]
        public async Task<IHttpActionResult> CreateToDo([FromBody, Required] ToDoViewModel toDoVm)
        {
            var toDoModel = Mapper.Map<ToDoViewModel, ToDo>(toDoVm);

            try
            {
                var createListResult = await _toDoService.CreateToDoAsync(toDoModel);
                return Ok(createListResult);
            }
            catch (ArgumentException)
            {
                return Ok(new { ErrorMessage = ToDoResources.CannotCreateError });
            }
        }

        /// <summary>
        /// An HTTP Post request to create a new to do item
        /// </summary>
        /// <param name="toDo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateToDo")]
        public async Task<IHttpActionResult> UpdateToDo([FromBody, Required] ToDoViewModel toDoVm)
        {
            var toDoModel = Mapper.Map<ToDoViewModel, ToDo>(toDoVm);
            
            try
            {
                var updateToDoResult = await _toDoService.UpdateToDoAsync(toDoModel);
                return Ok(updateToDoResult);
            }
            catch (ArgumentException)
            {
                return Ok(new { ErrorMessage = ToDoResources.CannotUpdateError });
            }
        }

        /// <summary>
        /// An HTTP Get request to retrieve all of the to do items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetToDos")]
        public async Task<IHttpActionResult> GetToDos()
        {
            var toDoItemsList = await _toDoService.GetToDoItemsAsync();

            return Ok(toDoItemsList);
        }
    }
}
