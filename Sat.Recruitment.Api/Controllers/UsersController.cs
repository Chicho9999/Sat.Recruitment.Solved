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
            var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var user = new UserBuilder(
                    name: line.Split(',')[0].ToString(),
                    email: line.Split(',')[1].ToString(),
                    address: line.Split(',')[3].ToString(),
                    phone: line.Split(',')[2].ToString(),
                    money: line.Split(',')[5].ToString()).Build(userType: line.Split(',')[4].ToString());

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