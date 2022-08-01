using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Helper;
using Sat.Recruitment.Api.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    /// <summary>
    /// Class that manage Users.
    /// </summary>
    [ApiController]
    [Route("api/user")]
    public class UsersController : ControllerBase
    {

        private readonly List<User> _users = new List<User>();

        /// <summary>
        /// API Rest that creates a new User. 
        /// </summary>
        /// <param name="userModel">Model of the User</param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-user/")]
        public async Task<Result> CreateUser([FromBody]UserModel userModel)
        {
            var errors = "";

            ErrorValidationHelper.ValidateErrors(userModel.Name, userModel.Email, userModel.Address, userModel.Phone, ref errors);

            if (errors != null && errors != "")
                return await Task.FromResult(new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                });

            var userBuilder = new UserBuilder(userModel.Name, userModel.Email, userModel.Address, userModel.Phone, userModel.Money);

            var newUser = userBuilder.Build(userModel.UserType);

            newUser.CalculatePercentage(userModel.Money);

            var reader = UserReader.ReadUsersFromFile();

            //Normalize email
            var emailSplited = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = emailSplited[0].IndexOf("+", StringComparison.Ordinal);

            emailSplited[0] = atIndex < 0 ? emailSplited[0].Replace(".", "") : emailSplited[0].Replace(".", "").Remove(atIndex);

            newUser.Email = string.Join("@", new string[] { emailSplited[0], emailSplited[1] });

            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var lineSplited = line.Split(',');

                var user = new UserBuilder(
                    name: lineSplited[0].ToString(),
                    email: lineSplited[1].ToString(),
                    address: lineSplited[3].ToString(),
                    phone: lineSplited[2].ToString(),
                    money: lineSplited[5].ToString()).Build(userType: lineSplited[4].ToString());

                _users.Add(user);
            }
            reader.Close();

            try
            {
                foreach (var user in _users)
                {
                    if ((user.Name == newUser.Name && user.Address == newUser.Address) || user.Email == newUser.Email || user.Phone == newUser.Phone)
                    {
                        throw new Exception("User is duplicated");
                    }
                }

                Debug.WriteLine("User Created");

                return await Task.FromResult(new Result()
                {
                    IsSuccess = true,
                    Errors = "User Created"
                });
            }
            catch
            {
                Debug.WriteLine("The user is duplicated");
                return await Task.FromResult(new Result()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                });
            }
        }
    }
}