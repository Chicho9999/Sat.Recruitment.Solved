using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Model;
using Sat.Recruitment.Api.Repository.Interface;
using Sat.Recruitment.Api.Service.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class TestUserCreation
    {

        [Fact]
        public async void TestCreateUserSuccess()
        {
            var iUserServiceMock = new Mock<IUserService>();
            iUserServiceMock.Setup(x => x.CreateUser(It.IsAny<User>())).Returns(Task.CompletedTask);
            var userController = new UsersController(iUserServiceMock.Object);

            var userModel = new UserModel()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = "124"
            };

            var result = await userController.CreateUser(userModel);
            var response = (result.Result as OkObjectResult).Value as Result;

            Assert.True(response.IsSuccess);
            Assert.Equal("User Created", response.Errors);
        }

        [Fact]
        public async Task TestCreateUserDuplicated()
        {
            var iUserServiceMock = new Mock<IUserService>();
            var userController = new UsersController(iUserServiceMock.Object);
            iUserServiceMock.Setup(x => x.CreateUser(It.IsAny<User>())).Throws(new System.Exception("User is duplicated"));

            var userModel = new UserModel()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = "124"
            };

            var result = await userController.CreateUser(userModel);
            var response = (result.Result as BadRequestObjectResult).Value as Result;

            Assert.False(response.IsSuccess);
            Assert.Equal("User is duplicated", response.Errors);
        }

        [Fact]
        public async Task TestCreateUserEmptyForm()
        {
            var iUserServiceMock = new Mock<IUserService>();
            var userController = new UsersController(iUserServiceMock.Object);

            var userModel = new UserModel() {};

            var result = await userController.CreateUser(userModel);
            var response = (result.Result as BadRequestObjectResult).Value as Result;


            Assert.False(response.IsSuccess);
            Assert.Equal("The name is required The email is required The address is required The phone is required", response.Errors);
        }
    }
}
