using System;
using Xunit;
using Moq;
using DiaryApi.Infrastructure;
using System.Collections.Generic;
using TodoListApi.Models;
using System.Threading.Tasks;
using TodoListApi.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace xUnitTests.TodoListApiTests
{
    public class xUnitTest
    {
        Mock<TodoListApi.Infrastructure.IRepo> mock = new Mock<TodoListApi.Infrastructure.IRepo>();

        private List<TodoModel> GetTestDbData()
        {
            return new List<TodoModel>
            {
                new TodoModel{ Id = 1, TaskName = "One", IsDone = false },
                new TodoModel{ Id = 2, TaskName = "Two", IsDone = false },
                new TodoModel{ Id = 3, TaskName = "Three", IsDone = false },
                new TodoModel{ Id = 4, TaskName = "Four", IsDone = true },
                new TodoModel{ Id = 5, TaskName = "Fice", IsDone = true },
            };
        }

        [Fact]
        public void TodoConteroller_GetAllMethod_Returns_AllTodoNotes_Result()
        {
            //Arrange
            mock.Setup(repo => repo.GetAllTodos()).Returns(Task.FromResult(GetTestDbData() as IEnumerable<TodoModel>));
            var controller = new TodoController(mock.Object);
            //Act
            var result = controller.GetAll();
            //Assert
            var viewResult = Assert.IsType<Task<IEnumerable<TodoModel>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<TodoModel>>(viewResult.Result);
            Assert.Equal(GetTestDbData().Count, model.Count());
        }

        [Fact]
        public void TodoConteroller_GetAlreadyFinishedMethod_Returns_AllTodoNotes_Result()
        {
            //Arrange
            mock.Setup(repo => repo.GetAlreadyFinishedTodos())
                .Returns(Task.FromResult(GetTestDbData().Where(i => i.IsDone == true) as IEnumerable<TodoModel>));
            var controller = new TodoController(mock.Object);
            //Act
            var result = controller.GetAllDone();
            //Assert
            var viewResult = Assert.IsType<Task<IEnumerable<TodoModel>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<TodoModel>>(viewResult.Result);
            Assert.Equal(GetTestDbData().Where(i => i.IsDone == true).Count(), model.Count());
        }

        [Fact]
        public void TodoController_CreateTodoNoteMethod_Returns_OkObject_Result()
        {
            //Arrange
            TodoModel testObject = new TodoModel { Id = 1, TaskName = "good", IsDone = false};

            mock.Setup(repo => repo.Create(testObject));
            var controller = new TodoController(mock.Object);
            //Act
            var result = controller.CreateTodo(testObject);
            //Assert
            Assert.IsType<OkObjectResult>(result);
            mock.Verify(r => r.Create(testObject));
        }

        [Fact]
        public void TodoController_UpdateTodoNoteMethod_Returns_NoContent_Result()
        {
            //Arrange
            int id = 1;
            TodoModel testObject = new TodoModel { Id = 1, TaskName = "good", IsDone = false };

            var controller = new TodoController(mock.Object);
            //Act
            var result = controller.UpdateTodo(id, testObject);
            //Assert
            Assert.IsType<NoContentResult>(result);
            mock.Verify(r => r.Update(testObject));
        }

        [Fact]
        public void TodoController_UpdateTodoNoteMethod_Returns_BadRequest_Result()
        {
            //Arrange
            int id = 1;
            TodoModel testObject = new TodoModel { Id = 2, TaskName = "good", IsDone = false };

            var controller = new TodoController(mock.Object);
            //Act
            var result = controller.UpdateTodo(id, testObject);
            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void TodoController_RemoveTodo_Returns_NoContent_Result()
        {
            //Arrange
            int id = 1;

            var controller = new TodoController(mock.Object);
            //Act
            var result = controller.RemoveTodo(id);
            //Assert
            Assert.IsType<NoContentResult>(result);
            mock.Verify(r => r.Delete(id));
        }
    }
}
