using SomeonesToDoListApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using NLog;
using AndresToDoListApp.DataAccessLayer.Interfaces;

namespace SomeonesToDoListApp.Services
{
    public class ToDoService : IToDoService
    {
        private IUnitOfWork _unitOfWork;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ToDoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ToDo> CreateToDoAsync(ToDo newToDo)
        {
            if (newToDo.ToDoItem == null)
            {
                throw new ArgumentException("ToDo item text cannot be null when creating a new item.");
            }

            try
            {
                var createdToDo = _unitOfWork.ToDoRepository.Add(newToDo);
                await _unitOfWork.Save();

                return createdToDo;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Error occured while adding a new ToDo item");
                throw;
            }
        }

        public async Task<bool> UpdateToDoAsync(ToDo toDo)
        {
            if (toDo.ToDoItem == null)
            {
                throw new ArgumentException("ToDo item text cannot be null when updating item.");
            }

            try
            {
                _unitOfWork.ToDoRepository.Update(toDo);

                await _unitOfWork.Save();
                return true;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Error occured while updating a ToDo item");
                throw;
            }
        }

        public async Task<IEnumerable<ToDo>> GetToDoItemsAsync()
        {
            try
            {
                var toDoModels = await _unitOfWork.ToDoRepository.GetAll();

                return toDoModels;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Error occured while getting all ToDo items");
                throw;
            }
        }
    }
}
