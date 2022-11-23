using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SomeonesToDoListApp.Tests.Base;
using SomeonesToDoListApp.Services.Interfaces;
using AndresToDoListApp.DataAccessLayer.Interfaces;
using AndresToDoListApp.DataAccessLayer;
using System;

namespace SomeonesToDoListApp.Tests.Services
{
    [TestClass]
    public class ToDoServiceTest : TestBase
    {
        private Mock<SomeonesToDoListContext> _contextMock;
        private IToDoService _toDoService;
        private IUnitOfWork _unitOfWork;
        private const int SAVE_RESULT = 3;

        [TestInitialize]
        public void Setup()
        {
            var testDate = GetTestData();

            var dbSetMock = new Mock<DbSet<ToDo>>();
            SetupMockSetAsync(dbSetMock, testDate);
            _contextMock = new Mock<SomeonesToDoListContext>();
            _contextMock.Setup(m => m.Set<ToDo>()).Returns(dbSetMock.Object);

            _unitOfWork = new UnitOfWork(_contextMock.Object);
            _toDoService = new ToDoService(_unitOfWork);
        }

        [TestMethod]
        public async Task CreateToDoAsync()
        {
            var newToDo = new ToDo
            {
                Id = 4,
                ToDoItem = "Find my lost cat"
            };

            var createdToDo = new ToDo
            {
                Id = 4,
                ToDoItem = "Find my lost cat"
            };

            _contextMock.Setup(m => m.Set<ToDo>().Add(newToDo)).Returns(createdToDo);
            _contextMock.Setup(m => m.SaveChangesAsync()).ReturnsAsync(SAVE_RESULT);

            // act
            var res = await _toDoService.CreateToDoAsync(newToDo);

            // assert
            Assert.AreEqual(createdToDo, res);
            _contextMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        public void CreateToDoAsync_NullToDoText_ThrowsArgumentException()
        {
            var newToDo = new ToDo
            {
                Id = 4
            };

            Assert.ThrowsExceptionAsync<ArgumentException>(() => _toDoService.CreateToDoAsync(newToDo));
        }

        [TestMethod]
        public void CreateToDoAsync_ExceptionFromDal_ReThrow()
        {
            var newToDo = new ToDo
            {
                Id = 4,
                ToDoItem = "Find my lost cat"
            };

            _contextMock.Setup(m => m.Set<ToDo>().Add(newToDo)).Throws(new ArgumentNullException());

            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _toDoService.CreateToDoAsync(newToDo));
        }

        [TestMethod]
        public async Task UpdateToDoAsync()
        {
            var newToDo = new ToDo
            {
                Id = 4,
                ToDoItem = "Find my lost cat"
            };

            _contextMock.Setup(m => m.Set<ToDo>().Attach(newToDo));
            _contextMock.Setup(m => m.SetModified(newToDo));
            _contextMock.Setup(m => m.SaveChangesAsync()).ReturnsAsync(SAVE_RESULT);

            // act
            var res = await _toDoService.UpdateToDoAsync(newToDo);

            // assert
            Assert.IsTrue(true);
            _contextMock.Verify(m => m.Set<ToDo>().Attach(newToDo), Times.Once);
            _contextMock.Verify(m => m.SetModified(newToDo), Times.Once);
        }

        [TestMethod]
        public void UpdateToDoAsync_NullToDoText_ThrowsArgumentException()
        {
            var newToDo = new ToDo
            {
                Id = 4
            };

            Assert.ThrowsExceptionAsync<ArgumentException>(() => _toDoService.UpdateToDoAsync(newToDo));
        }

        [TestMethod]
        public void UpdateToDoAsync_ExceptionFromDal_ReThrow()
        {
            var newToDo = new ToDo
            {
                Id = 4,
                ToDoItem = "Find my lost cat"
            };

            _contextMock.Setup(m => m.Set<ToDo>().Add(newToDo)).Throws(new ArgumentNullException());

            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _toDoService.UpdateToDoAsync(newToDo));
        }

        [TestMethod]
        public async Task GetToDoItemsAsync()
        {
            // act
            var res = await _toDoService.GetToDoItemsAsync();

            // assert
            Assert.AreEqual(res.Count(), 3);
            Assert.AreEqual(res.First().Id, 1);
            Assert.AreEqual(res.Last().ToDoItem, "Do my homework");
        }
    }
}
