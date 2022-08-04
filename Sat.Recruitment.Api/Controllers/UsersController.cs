using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Helper;
using Sat.Recruitment.Api.Model;
using Sat.Recruitment.Api.Service.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    /// <summary>
    /// Class that manage Users.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        public IUserService UserService { get; }
        
        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        /// <summary>
        /// API Rest that creates a new User. 
        /// </summary>
        /// <param name="userModel">Model of the User</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Result>> CreateUser([FromBody] UserModel userModel)
        {
            var errors = ErrorValidationHelper.ValidateErrors(userModel.Name, userModel.Email, userModel.Address, userModel.Phone);

            try
            {
                if (errors != null && errors != "")
                    throw new Exception(errors);

                var userBuilder = new UserBuilder(userModel.Name, userModel.Email, userModel.Address, userModel.Phone, userModel.Money);

                var newUser = userBuilder.Build(userModel.UserType);

                newUser.CalculatePercentage(userModel.Money);

                await UserService.CreateUser(newUser);

                return await Task.FromResult(Ok(new Result()
                {
                    IsSuccess = true,
                    Errors = "User Created"
                }));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return await Task.FromResult(BadRequest(new Result()
                {
                    IsSuccess = false,
                    Errors = ex.Message
                }));
            }
        }
    }
}