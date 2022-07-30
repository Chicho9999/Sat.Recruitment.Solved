using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Helper;
using Sat.Recruitment.Api.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly List<User> _users = new List<User>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Name of the user</param>
        /// <param name="email">Email of the user</param>
        /// <param name="address">Address of the user</param>
        /// <param name="phone">Phone Number of the user</param>
        /// <param name="userType">Type of the user</param>
        /// <param name="money"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var errors = "";

            ErrorValidationHelper.ValidateErrors(name, email, address, phone, ref errors);

            if (errors != null && errors != "")
                return await Task.FromResult(new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                });

            var userBuilder = new UserBuilder(name, email, address, phone, money);

            var newUser = userBuilder.Build(userType);

            newUser.CalculatePercentage(money);

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
                    if (user.Email == newUser.Email || user.Name == newUser.Name || user.Address == newUser.Address || user.Phone == newUser.Phone)
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