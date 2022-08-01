using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Model;
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
            var userController = new UsersController();

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


            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public async Task TestCreateUserDuplicated()
        {
            var userController = new UsersController();
            
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


            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Fact]
        public async Task TestCreateUserEmptyForm()
        {
            var userController = new UsersController();

            var userModel = new UserModel() {};

            var result = await userController.CreateUser(userModel);


            Assert.False(result.IsSuccess);
            Assert.Equal("The name is required The email is required The address is required The phone is required", result.Errors);
        }
    }
}
