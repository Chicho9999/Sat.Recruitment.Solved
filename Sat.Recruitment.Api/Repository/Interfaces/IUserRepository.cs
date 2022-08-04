using Sat.Recruitment.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();

    }
}
