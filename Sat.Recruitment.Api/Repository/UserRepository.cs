using Sat.Recruitment.Api.Helper;
using Sat.Recruitment.Api.Model;
using Sat.Recruitment.Api.Repository.Interface;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private StreamReader reader;

        private readonly List<User> _users = new List<User>();

        public async Task<List<User>> GetAllAsync()
        {
            reader = UserReader.ReadUsersFromFile();

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

            return _users;
        }
    }
}
