using Xunit;
using Moq;
using IdentityApi.Models;
using System.Threading.Tasks;
using IdentityApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace xUnitTests.IdentityApiTests
{
    public class xUnitTest
    {
        Mock<IdentityApi.Services.IAuthService> mock = new Mock<IdentityApi.Services.IAuthService>();

        [Fact]
        public void AuthController_RegisterMethod_Returns_AddedObject()
        {
            //Arrange
            User model = new User { _id = "12abc", Login = "testLog", Password = "TestPass", UserName = "UserTest", UserAge = 10 };

            mock.Setup(repo => repo.RegisterUser(model)).Returns(Task.FromResult(model));
            var controller = new AuthController(mock.Object);
            //Act
            var result = controller.Register(model);
            //Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
            var modelResult = Assert.IsAssignableFrom<JsonResult>(viewResult.Result);
            Assert.Equal(model, modelResult.Value);
            mock.Verify(repo => repo.RegisterUser(model));
        }

        [Fact]
        public void AuthController_AuthenticateUserByModelMethod_Returns_FoundObject()
        {
            //Arrange
            User model = new User { Login = "testLog", Password = "testPass" };

            mock.Setup(repo => repo.AuthenticateUser(model)).Returns(Task.FromResult(model));
            var controller = new AuthController(mock.Object);
            //Act
            var result = controller.AuthenticateUserByModel(model);
            //Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
            var modelResult = Assert.IsAssignableFrom<JsonResult>(viewResult.Result);
            Assert.Equal(model, modelResult.Value);
        }

        [Fact]
        public void AuthController_AuthenticateUserByModelMethod_Returns_JsonAsNullableObject()
        {
            //Arrange
            User model = new User { Login = "testLog", Password = "testPass" };

            mock.Setup(repo => repo.AuthenticateUser(model)).Returns(Task.FromResult(null as User));
            var controller = new AuthController(mock.Object);
            //Act
            var result = controller.AuthenticateUserByModel(model);
            //Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
            var modelResult = Assert.IsAssignableFrom<JsonResult>(viewResult.Result);
        }

        [Fact]
        public void AuthController_FindUserByIdMethod_Returns_JsonObject()
        {
            //Arrange
            User model = new User { _id = "12abc", Login = "testLog", Password = "TestPass", UserName = "UserTest", UserAge = 10 };

            mock.Setup(repo => repo.FindUserById(model._id)).Returns(Task.FromResult(model));
            var controller = new AuthController(mock.Object);
            //Act
            var result = controller.FindUser(model._id);
            //Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
            var modelResult = Assert.IsAssignableFrom<JsonResult>(viewResult.Result);
            Assert.Equal(model, modelResult.Value);
        }

        [Fact]
        public void AuthController_FindUserByIdMethod_Returns_NotFoundResult()
        {
            //Arrange
            string _id = "123qwe";

            mock.Setup(repo => repo.FindUserById(_id)).Returns(Task.FromResult(null as User));
            var controller = new AuthController(mock.Object);
            //Act
            var result = controller.FindUser(_id);
            //Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
            var modelResult = Assert.IsAssignableFrom<NotFoundResult>(viewResult.Result);
        }

        [Fact]
        public void AuthController_UpdateInfodMethod_Returns_NotFoundResult()
        {
            //Arrange
            User oldModel = new User { _id = "12abc", Login = "testLog", Password = "TestPass", UserName = "UserTest", UserAge = 10 };
            
            mock.Setup(repo => repo.UpdateUserInfo(oldModel)).Returns(Task.FromResult(null as User));
            var controller = new AuthController(mock.Object);
            //Act
            var result = controller.UpdateInfo(oldModel);
            //Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
            var modelResult = Assert.IsAssignableFrom<NotFoundResult>(viewResult.Result);
        }

        [Fact]
        public void AuthController_UpdateInfodMethod_Returns_JsonObject()
        {
            //Arrange
            User oldModel = new User { _id = "12abc", Login = "testLog", Password = "TestPass", UserName = "UserTest", UserAge = 10 };
            User updatedModel = new User
            {
                _id = "12update",
                Login = "testLogUpdated",
                Password = "TestPass",
                UserName = "UserTestUpdated",
                UserAge = 11
            };

            mock.Setup(repo => repo.UpdateUserInfo(oldModel)).Returns(Task.FromResult(updatedModel));
            var controller = new AuthController(mock.Object);
            //Act
            var result = controller.UpdateInfo(oldModel);
            //Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
            var modelResult = Assert.IsAssignableFrom<JsonResult>(viewResult.Result);
            Assert.Equal(updatedModel, modelResult.Value);
        }
    }
}
