using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http.Results;
using SomeonesToDoListApp.Controllers;
using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.Services;
using SomeonesToDoListApp.Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using SomeonesToDoListApp.AutoMapper;
using SomeonesToDoListApp.ViewModels;
using SomeonesToDoListApp.Services.Interfaces;

namespace SomeonesToDoListApp.Tests.Controllers
{
    [TestClass]
    public class ToDoControllerTest : TestBase
    {
        private Mock<IToDoService> _toDoServiceMock;

        [ClassInitialize]
        public void InitializeAutoMapper()
        {
            _toDoServiceMock = new Mock<IToDoService>();
        }

        [TestMethod]
        public async Task CreateToDoAsyncControllerTest()
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

            // act
            var toDoController = new ToDoController(_toDoServiceMock.Object);

            var controllerActionResult = await toDoController.CreateToDo(newToDo);

            // assert
            Assert.IsInstanceOfType(controllerActionResult, typeof(OkNegotiatedContentResult<bool>));
        }

        [TestMethod]
        public async Task GetToDoItemsAsyncControllerTest()
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

            // act
            var toDoController = new ToDoController(_toDoServiceMock.Object);

            var controllerActionResult = await toDoController.GetToDos();

            // assert
            Assert.IsInstanceOfType(controllerActionResult, typeof(OkNegotiatedContentResult<IEnumerable<ToDoViewModel>>));
        }
    }
}