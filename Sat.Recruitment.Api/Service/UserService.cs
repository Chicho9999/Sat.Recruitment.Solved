using Sat.Recruitment.Api.Helper;
using Sat.Recruitment.Api.Model;
using Sat.Recruitment.Api.Repository.Interface;
using Sat.Recruitment.Api.Service.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Service
{
    public class UserService : IUserService
    {
        public IUserRepository UserRepository { get; }

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        public async Task CreateUser(UserModel userModel)
        {
            var userBuilder = new UserBuilder(userModel.Name, userModel.Email, userModel.Address, userModel.Phone, userModel.Money);

            var newUser = userBuilder.Build(userModel.UserType);

            newUser.CalculatePercentage(userModel.Money);

            //Normalize email
            var emailSplited = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = emailSplited[0].IndexOf("+", StringComparison.Ordinal);

            emailSplited[0] = atIndex < 0 ? emailSplited[0].Replace(".", "") : emailSplited[0].Replace(".", "").Remove(atIndex);

            newUser.Email = string.Join("@", new string[] { emailSplited[0], emailSplited[1] });

            var users = await UserRepository.GetAllAsync();

            foreach (var user in users)
            {
                if ((user.Name == newUser.Name && user.Address == newUser.Address) || user.Email == newUser.Email || user.Phone == newUser.Phone)
                {
                    throw new Exception("User is duplicated");
                }
            }

            Debug.WriteLine("User Created");
        }
    }
}