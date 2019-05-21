using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using DiaryApi.Models;
using DiaryApi.Infrastructure;
using System.Threading.Tasks;
using DiaryApi.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace xUnitTests.DiaryApiTests
{
    public class xUnitTest
    {
        Mock<DiaryApi.Infrastructure.IRepo> mock = new Mock<DiaryApi.Infrastructure.IRepo>();

        //Test Database Data
        private List<DiaryModel> GetTestDbData()
        {
            return new List<DiaryModel>
            {
                new DiaryModel{ Id = 1, Date = "23.02.2001", DayDescription = "Day One", DayMark = "One" },
                new DiaryModel{ Id = 2, Date = "23.02.2001", DayDescription = "Day Two", DayMark = "Two" },
                new DiaryModel{ Id = 3, Date = "23.02.2001", DayDescription = "Day Three", DayMark = "Three" },
                new DiaryModel{ Id = 4, Date = "23.02.2001", DayDescription = "Day Four", DayMark = "Four" }
            };
        }

        [Fact]
        public void DiaryController_GetAllMethod_Returns_AllData_From_Table()
        {
            //Arrange
            
            mock.Setup(repo => repo.GetAllDiaryNotes()).Returns(Task.FromResult(GetTestDbData() as IEnumerable<DiaryModel>));
            var controller = new DiaryController(mock.Object);
            //Act
            var result = controller.GetAll();
            //Assert
            var viewResult = Assert.IsType<Task<IEnumerable<DiaryModel>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<DiaryModel>>(viewResult.Result);
            Assert.Equal(GetTestDbData().Count, model.Count());
            
        }

        [Fact]
        public void DiaryController_GetCurrentMethod_Returns_JSON_Object()
        {
            //Arrange
            DiaryModel testObject = new DiaryModel { Id = 1, Date = "23.02.2001", DayMark = "good day",
                                                      DayDescription = "I wrote some xUnit tests!" };

            mock.Setup(repo => repo.GetCurrentDiaryNote(testObject.Id)).Returns(Task.FromResult(testObject));
            var controller = new DiaryController(mock.Object);
            //Act
            var result = controller.GetCurrent(testObject.Id);
            //Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
            var model = Assert.IsAssignableFrom<JsonResult>(viewResult.Result);
            Assert.Equal(testObject, model.Value);
        }

        [Fact]
        public void DiaryController_GetCurrentMethod_Returns_NotFoundObject_Result()
        {
            //Arrange
            int id = 1;

            mock.Setup(repo => repo.GetCurrentDiaryNote(id)).Returns(Task.FromResult(null as DiaryModel));
            var controller = new DiaryController(mock.Object);
            //Act
            var result = controller.GetCurrent(id);
            //Assert
            var view = Assert.IsType<Task<IActionResult>>(result);
            Assert.IsAssignableFrom<NotFoundObjectResult>(view.Result);
        }

        [Fact]
        public void DiaryController_CreateDiaryNoteMethod_Returns_OkObject_Result()
        {
            //Arrange
            DiaryModel testObject = new DiaryModel { Id = 1, Date = "23.02.2001", DayMark = "good", DayDescription = "text" };

            mock.Setup(repo => repo.Create(testObject));
            var controller = new DiaryController(mock.Object);
            //Act
            var result = controller.CreateDiaryNote(testObject);
            //Assert
            Assert.IsType<OkObjectResult>(result);
            mock.Verify(r => r.Create(testObject));
        }

        [Fact]
        public void DiaryController_UpdateDiary_Returns_NoContent_Result()
        {
            //Arrange
            int id = 1;
            DiaryModel testObject = new DiaryModel { Id = id, Date = "23.02.2001", DayMark = "good", DayDescription = "text" };

            var controller = new DiaryController(mock.Object);
            //Act
            var result = controller.UpdateDiary(id, testObject);
            //Assert
            Assert.IsType<NoContentResult>(result);
            mock.Verify(r => r.Update(testObject));
        }

        [Fact]
        public void DiaryController_UpdateDiary_Returns_BadRequest_Result()
        {
            //Arrange
            int id = 1;
            DiaryModel testObject = new DiaryModel { Id = 2, Date = "23.02.2001", DayMark = "good", DayDescription = "text" };

            var controller = new DiaryController(mock.Object);
            //Act
            var result = controller.UpdateDiary(id, testObject);
            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DiaryController_RemoveDiaryNote_Returns_NoContent_Result()
        {
            //Arrange
            int id = 1;

            var controller = new DiaryController(mock.Object);
            //Act
            var result = controller.RemoveDiaryNote(id);
            //Assert
            Assert.IsType<NoContentResult>(result);
            mock.Verify(r => r.Delete(id));
        }
    }
}
