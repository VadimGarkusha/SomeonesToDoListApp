using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http.Results;
using SomeonesToDoListApp.Controllers;
using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using SomeonesToDoListApp.AutoMapper;
using SomeonesToDoListApp.ViewModels;
using SomeonesToDoListApp.Services.Interfaces;
using System;

namespace SomeonesToDoListApp.Tests.Controllers
{
    [TestClass]
    public class ToDoControllerTest : TestBase
    {
        private Mock<IToDoService> _toDoServiceMock;
        private ToDoController _toDoController;

        [TestInitialize]
        public void Setup()
        { 
            _toDoServiceMock = new Mock<IToDoService>();
            _toDoController = new ToDoController(_toDoServiceMock.Object);
        }

        [TestMethod]
        public async Task CreateToDo()
        {
            var newToDo = new ToDoViewModel
            {
                Id = 4,
                ToDoItem = "Find my lost cat"
            };

            var createdToDo = new ToDo
            {
                Id = 4,
                ToDoItem = "Find my lost cat"
            };

            _toDoServiceMock.Setup(m => m.CreateToDoAsync(It.IsAny<ToDo>())).ReturnsAsync(createdToDo);

            var controllerActionResult = await _toDoController.CreateToDo(newToDo);
            Assert.IsInstanceOfType(controllerActionResult, typeof(OkNegotiatedContentResult<ToDo>));

            var result = controllerActionResult as OkNegotiatedContentResult<ToDo>;

            Assert.AreEqual(result.Content.Id, createdToDo.Id);
            Assert.AreEqual(result.Content.ToDoItem, createdToDo.ToDoItem);
        }

        [TestMethod]
        public async Task CreateToDo_ArgumentException_ReturnErrorMessage()
        {
            // arrange
            var toDos = GetTestData();

            var mockToDoSet = SetupMockSetAsync(new Mock<DbSet<ToDo>>(), toDos);
            var mockContext = new Mock<SomeonesToDoListContext>();

            mockContext.Setup(s => s.ToDos).Returns(mockToDoSet.Object);

            var newToDo = new ToDoViewModel
            {
                Id = 4,
                ToDoItem = "Find my lost cat"
            };

            var createdToDo = new ToDo
            {
                Id = 4,
                ToDoItem = "Find my lost cat"
            };

            _toDoServiceMock.Setup(m => m.CreateToDoAsync(It.IsAny<ToDo>())).ThrowsAsync(new ArgumentException());

            // act
            var controllerActionResult = await _toDoController.CreateToDo(newToDo);
            Assert.IsTrue(controllerActionResult.GetType().GetGenericTypeDefinition() == typeof(OkNegotiatedContentResult<>));
        }
    }
}