using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.Services.Interfaces;
using SomeonesToDoListApp.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using System.Data.Entity;
using NLog;
using AndresToDoListApp.Services.Interfaces;
using AndresToDoListApp.Services.ViewModels;
using AndresToDoListApp.Services;

namespace SomeonesToDoListApp.Services
{
	public class ToDoService : IToDoService
	{
		private SomeonesToDoListContext _someonesToDoListContext { get; set; }
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public ToDoService(SomeonesToDoListContext someonesToDoListContext)
		{
			_someonesToDoListContext = someonesToDoListContext;
		}

		public async Task<IServiceResult<ToDoViewModel>> CreateToDoAsync(ToDoViewModel toDoViewModel)
		{
            var result = new ServiceResult<ToDoViewModel>(null);

            if (toDoViewModel.ToDoItem == null)
			{
				result.ErrorMessage = ToDoResources.CannotCreateError;
				return result;
            }

            try
			{
				var toDo = Mapper.Map<ToDoViewModel, ToDo>(toDoViewModel);

				var createdToDo = _someonesToDoListContext.ToDos.Add(toDo);

				await _someonesToDoListContext.SaveChangesAsync();

                result.Result = Mapper.Map<ToDo, ToDoViewModel>(createdToDo);
				return result;
			}
			catch (Exception exception)
			{
				_logger.Error(exception, "Error occured while adding a new ToDo item");
				throw;
			}
		}

        public async Task<IServiceResult<bool>> UpdateToDoAsync(ToDoViewModel toDoViewModel)
		{
            var toDo = Mapper.Map<ToDoViewModel, ToDo>(toDoViewModel);
			var result = new ServiceResult<bool>(false);


            if (toDoViewModel.ToDoItem == null)
			{
                _logger.Error("Tried to update ToDo with null ID");
				result.ErrorMessage = ToDoResources.CannotUpdateError;
				return result;
            }

            try
            {
				var modelToUpdate = await _someonesToDoListContext.ToDos.FindAsync(toDo.Id);
				if(modelToUpdate == null) 
				{
                    _logger.Error("Tried to update ToDo with null ID");
                    result.ErrorMessage = ToDoResources.CannotUpdateError;
                    return result;
                }

				_someonesToDoListContext.Entry(modelToUpdate).CurrentValues.SetValues(toDo);

                await _someonesToDoListContext.SaveChangesAsync();
                result.Result = true;
                return result;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Error occured while updating a ToDo item");
                throw;
            }
        }

        public async Task<IEnumerable<ToDoViewModel>> GetToDoItemsAsync()
		{
			try
			{
				var toDoModels = await _someonesToDoListContext.ToDos.ToListAsync();

                return Mapper.Map<IEnumerable<ToDo>, IEnumerable<ToDoViewModel>>(toDoModels);
			}
			catch (Exception exception)
			{
				_logger.Error(exception, "Error occured while getting all ToDo items");
				throw;
			}
		}
	}
}
