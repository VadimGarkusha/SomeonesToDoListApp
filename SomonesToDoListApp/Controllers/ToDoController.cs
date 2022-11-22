using SomeonesToDoListApp.Services.Interfaces;
using SomeonesToDoListApp.Services.ViewModels;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;
using System.ComponentModel.DataAnnotations;

namespace SomeonesToDoListApp.Controllers
{
	[RoutePrefix("ToDo")]
	public class ToDoController : ApiController
	{
		IToDoService _toDoService { get; set; }

		public ToDoController(IToDoService toDoService)
		{
			_toDoService = toDoService;
		}

		/// <summary>
		/// An HTTP Post request to create a new to do item
		/// </summary>
		/// <param name="toDo"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("CreateToDo")]
		public async Task<IHttpActionResult> CreateToDo([FromBody, Required] ToDoViewModel toDo)
		{
			var createListResult = await _toDoService.CreateToDoAsync(toDo);

			return Ok(createListResult);
		}

        /// <summary>
        /// An HTTP Post request to create a new to do item
        /// </summary>
        /// <param name="toDo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateToDo")]
        public async Task<IHttpActionResult> UpdateToDo([FromBody, Required] ToDoViewModel toDo)
        {
            var createListResult = await _toDoService.UpdateToDoAsync(toDo);

            return Ok(createListResult);
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
